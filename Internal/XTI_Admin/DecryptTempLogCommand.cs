using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XTI_Core;
using XTI_TempLog;
using XTI_TempLog.Abstractions;

namespace XTI_Admin;

internal sealed class DecryptTempLogCommand : ICommand
{
    private readonly Scopes scopes;

    public DecryptTempLogCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        var clock = scopes.GetRequiredService<IClock>();
        var tempLogs = scopes.GetRequiredService<ITempLogs>();
        var xtiFolder = scopes.GetRequiredService<XtiFolder>();
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
        var logEvents = await deserializeFiles<LogEventModel>(logEventFiles);
        logBatch.LogEvents = logEvents.OrderByDescending(evt => evt.TimeOccurred).ToArray();
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
