using System.Text.Json;
using XTI_Core;
using XTI_TempLog;
using XTI_TempLog.Abstractions;

namespace XTI_Admin;

internal sealed class DecryptTempLogCommand : ICommand
{
    private readonly IClock clock;
    private readonly ITempLogs tempLogs;
    private readonly XtiFolder xtiFolder;

    public DecryptTempLogCommand(IClock clock, ITempLogs tempLogs, XtiFolder xtiFolder)
    {
        this.clock = clock;
        this.tempLogs = tempLogs;
        this.xtiFolder = xtiFolder;
    }

    public async Task Execute(CancellationToken ct)
    {
        var modifiedBefore = clock.Now();
        var logs = tempLogs.Logs();
        var logBatch = await processBatch(logs, modifiedBefore);
        var folder = xtiFolder.AppDataFolder()
            .WithSubFolder("DecryptedTempLog");
        folder.TryCreate();
        var path = folder.FilePath($"decrypted_{DateTime.Now:yyMMddHHmmssfff}.json");
        using var writer = new StreamWriter(path, false);
        var serialized = JsonSerializer.Serialize(logBatch, new JsonSerializerOptions { WriteIndented = true });
        await writer.WriteAsync(serialized);
    }

    private async Task<LogBatchModel> processBatch(IEnumerable<TempLog> logs, DateTimeOffset modifiedBefore)
    {
        var logBatch = new LogBatchModel();
        var startSessionFiles = startProcessingFiles(logs, l => l.StartSessionFiles(modifiedBefore));
        var startSessions = await deserializeFiles<StartSessionModel>(startSessionFiles);
        startSessions = startSessions.OrderByDescending(sess => sess.TimeStarted).ToArray();
        logBatch.StartSessions = startSessions;
        var startRequestFiles = startProcessingFiles(logs, l => l.StartRequestFiles(modifiedBefore));
        var startRequests = await deserializeFiles<StartRequestModel>(startRequestFiles);
        startRequests = startRequests.OrderByDescending(req => req.TimeStarted).ToArray();
        logBatch.StartRequests = startRequests;
        var authSessionFiles = startProcessingFiles(logs, l => l.AuthSessionFiles(modifiedBefore));
        logBatch.AuthenticateSessions = await deserializeFiles<AuthenticateSessionModel>(authSessionFiles);
        var logEventFiles = startProcessingFiles(logs, l => l.LogEventFiles(modifiedBefore));
        var logEvents = await deserializeFiles<LogEntryModel>(logEventFiles);
        logBatch.LogEntries = logEvents.OrderByDescending(evt => evt.TimeOccurred).ToArray();
        var endRequestFiles = startProcessingFiles(logs, l => l.EndRequestFiles(modifiedBefore));
        logBatch.EndRequests = await deserializeFiles<EndRequestModel>(endRequestFiles);
        var endSessionFiles = startProcessingFiles(logs, l => l.EndSessionFiles(modifiedBefore));
        logBatch.EndSessions = await deserializeFiles<EndSessionModel>(endSessionFiles);
        return logBatch;
    }

    private IEnumerable<ITempLogFile> startProcessingFiles(IEnumerable<TempLog> logs, Func<TempLog, IEnumerable<ITempLogFile>> getFiles)
        => logs.SelectMany(l => getFiles(l));

    private async Task<T[]> deserializeFiles<T>(IEnumerable<ITempLogFile> files)
        where T : new()
    {
        var deserialized = new List<T>();
        foreach (var file in files)
        {
            try
            {
                var model = await deserializeFile<T>(file);
                deserialized.Add(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to deserialize file {file.Name}\r\n{ex}");
            }
        }
        return deserialized.ToArray();
    }

    private static async Task<T> deserializeFile<T>(ITempLogFile file)
        where T : new()
    {
        var content = await file.Read();
        return JsonSerializer.Deserialize<T>(content) ?? new T();
    }
}
