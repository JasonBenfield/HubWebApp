using System.Text.Json;
using XTI_Core;
using XTI_TempLog;
using XTI_TempLog.Abstractions;

namespace XTI_Admin;

internal sealed class DecryptTempLogCommand : ICommand
{
    private readonly IClock clock;
    private readonly ITempLogsV1 tempLogs;
    private readonly TempLog tempLog;
    private readonly XtiFolder xtiFolder;

    public DecryptTempLogCommand(IClock clock, ITempLogsV1 tempLogs, TempLog tempLog, XtiFolder xtiFolder)
    {
        this.clock = clock;
        this.tempLogs = tempLogs;
        this.tempLog = tempLog;
        this.xtiFolder = xtiFolder;
    }

    public async Task Execute(CancellationToken ct)
    {
        var modifiedBefore = clock.Now();
        var appDataFolder = xtiFolder.AppDataFolder()
            .WithSubFolder("DecryptedTempLog");
        appDataFolder.TryCreate();
        var sessionDetails = await GetSessionDetails(modifiedBefore);
        await WriteSessionDetails(appDataFolder, sessionDetails);
        var logs = tempLogs.Logs();
        var logBatch = await ProcessBatch(logs, modifiedBefore);
        await WriteBatch(appDataFolder, logBatch);
    }

    private async Task<TempLogSessionDetailModel[]> GetSessionDetails(DateTimeOffset modifiedBefore)
    {
        var logFiles = tempLog.Files(modifiedBefore);
        var sessionDetails = new List<TempLogSessionDetailModel>();
        foreach(var logFile in logFiles)
        {
            var fileSessionDetails = await logFile.Read();
            sessionDetails.AddRange(fileSessionDetails);
        }
        return sessionDetails.ToArray();
    }

    private static async Task WriteSessionDetails(AppDataFolder folder, TempLogSessionDetailModel[] sessionDetails)
    {
        var path = folder.FilePath($"decrypted_{DateTime.Now:yyMMddHHmmssfff}.json");
        using var writer = new StreamWriter(path, false);
        var serialized = JsonSerializer.Serialize(sessionDetails, new JsonSerializerOptions { WriteIndented = true });
        await writer.WriteAsync(serialized);
    }

    private async Task<LogBatchModel> ProcessBatch(IEnumerable<TempLogV1> logs, DateTimeOffset modifiedBefore)
    {
        var logBatch = new LogBatchModel();
        var startSessionFiles = StartProcessingFiles(logs, l => l.StartSessionFiles(modifiedBefore));
        var startSessions = await DeserializeFiles<StartSessionModel>(startSessionFiles);
        startSessions = startSessions.OrderByDescending(sess => sess.TimeStarted).ToArray();
        logBatch.StartSessions = startSessions;
        var startRequestFiles = StartProcessingFiles(logs, l => l.StartRequestFiles(modifiedBefore));
        var startRequests = await DeserializeFiles<StartRequestModel>(startRequestFiles);
        startRequests = startRequests.OrderByDescending(req => req.TimeStarted).ToArray();
        logBatch.StartRequests = startRequests;
        var authSessionFiles = StartProcessingFiles(logs, l => l.AuthSessionFiles(modifiedBefore));
        logBatch.AuthenticateSessions = await DeserializeFiles<AuthenticateSessionModel>(authSessionFiles);
        var logEventFiles = StartProcessingFiles(logs, l => l.LogEventFiles(modifiedBefore));
        var logEvents = await DeserializeFiles<LogEntryModelV1>(logEventFiles);
        logBatch.LogEntries = logEvents.OrderByDescending(evt => evt.TimeOccurred).ToArray();
        var endRequestFiles = StartProcessingFiles(logs, l => l.EndRequestFiles(modifiedBefore));
        logBatch.EndRequests = await DeserializeFiles<EndRequestModel>(endRequestFiles);
        var endSessionFiles = StartProcessingFiles(logs, l => l.EndSessionFiles(modifiedBefore));
        logBatch.EndSessions = await DeserializeFiles<EndSessionModel>(endSessionFiles);
        return logBatch;
    }

    private IEnumerable<ITempLogFileV1> StartProcessingFiles(IEnumerable<TempLogV1> logs, Func<TempLogV1, IEnumerable<ITempLogFileV1>> getFiles)
        => logs.SelectMany(l => getFiles(l));

    private async Task<T[]> DeserializeFiles<T>(IEnumerable<ITempLogFileV1> files)
        where T : new()
    {
        var deserialized = new List<T>();
        foreach (var file in files)
        {
            try
            {
                var model = await DeserializeFile<T>(file);
                deserialized.Add(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to deserialize file {file.Name}\r\n{ex}");
            }
        }
        return deserialized.ToArray();
    }

    private static async Task<T> DeserializeFile<T>(ITempLogFileV1 file)
        where T : new()
    {
        var content = await file.Read();
        return JsonSerializer.Deserialize<T>(content) ?? new T();
    }

    private static async Task WriteBatch(AppDataFolder folder, LogBatchModel logBatch)
    {
        var path = folder.FilePath($"decrypted_v1_{DateTime.Now:yyMMddHHmmssfff}.json");
        using var writer = new StreamWriter(path, false);
        var serialized = JsonSerializer.Serialize(logBatch, new JsonSerializerOptions { WriteIndented = true });
        await writer.WriteAsync(serialized);
    }

}
