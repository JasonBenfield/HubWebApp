﻿using System.Text.Json;
using XTI_Core;
using XTI_TempLog;
using XTI_TempLog.Abstractions;

namespace XTI_PermanentLog;

public sealed class TempToPermanentLog
{
    private readonly ITempLogs tempLogs;
    private readonly IPermanentLogClient permanentLogClient;
    private readonly IClock clock;
    private readonly int processMinutesBefore = 1;
    private readonly List<ITempLogFile> filesInProgress = new();

    public TempToPermanentLog(ITempLogs tempLogs, IPermanentLogClient permanentLogClient, IClock clock, int processMinutesBefore = 1)
    {
        this.tempLogs = tempLogs;
        this.permanentLogClient = permanentLogClient;
        this.clock = clock;
        this.processMinutesBefore = processMinutesBefore;
    }

    public async Task Move()
    {
        var modifiedBefore = clock.Now().AddMinutes(-processMinutesBefore);
        var logs = tempLogs.Logs();
        filesInProgress.Clear();
        var logBatch = await processBatch(logs, modifiedBefore);
        while (hasAnyToProcess(logBatch))
        {
            await permanentLogClient.LogBatch(logBatch);
            deleteFiles(filesInProgress);
            filesInProgress.Clear();
            logBatch = await processBatch(logs, modifiedBefore);
        }
    }

    private static bool hasAnyToProcess(LogBatchModel logBatch)
    {
        return logBatch.StartSessions.Any()
            || logBatch.StartRequests.Any()
            || logBatch.AuthenticateSessions.Any()
            || logBatch.LogEntries.Any()
            || logBatch.EndRequests.Any()
            || logBatch.EndSessions.Any();
    }

    private async Task<LogBatchModel> processBatch(IEnumerable<TempLog> logs, DateTimeOffset modifiedBefore)
    {
        var logBatch = new LogBatchModel();
        var startSessionFiles = startProcessingFiles(logs, l => l.StartSessionFiles(modifiedBefore));
        logBatch.StartSessions = await deserializeFiles<StartSessionModel>(startSessionFiles);
        var startRequestFiles = startProcessingFiles(logs, l => l.StartRequestFiles(modifiedBefore));
        logBatch.StartRequests = await deserializeFiles<StartRequestModel>(startRequestFiles);
        var authSessionFiles = startProcessingFiles(logs, l => l.AuthSessionFiles(modifiedBefore));
        logBatch.AuthenticateSessions = await deserializeFiles<AuthenticateSessionModel>(authSessionFiles);
        var logEventFiles = startProcessingFiles(logs, l => l.LogEventFiles(modifiedBefore));
        logBatch.LogEntries = await deserializeFiles<LogEntryModel>(logEventFiles);
        var endRequestFiles = startProcessingFiles(logs, l => l.EndRequestFiles(modifiedBefore));
        logBatch.EndRequests = await deserializeFiles<EndRequestModel>(endRequestFiles);
        var endSessionFiles = startProcessingFiles(logs, l => l.EndSessionFiles(modifiedBefore));
        logBatch.EndSessions = await deserializeFiles<EndSessionModel>(endSessionFiles);
        return logBatch;
    }

    private IEnumerable<ITempLogFile> startProcessingFiles(IEnumerable<TempLog> logs, Func<TempLog, IEnumerable<ITempLogFile>> getFiles)
    {
        var filesToProcess = logs
            .SelectMany(l => getFiles(l))
            .Take(50)
            .Select(f => f.WithNewName($"{f.Name}.processing"))
            .ToArray();
        filesInProgress.AddRange(filesToProcess);
        return filesToProcess;
    }

    private async Task<T[]> deserializeFiles<T>(IEnumerable<ITempLogFile> files)
        where T : new()
    {
        var deserialized = new List<T>();
        foreach (var file in files)
        {
            var content = await file.Read();
            var model = JsonSerializer.Deserialize<T>(content);
            deserialized.Add(model ?? new T());
        }
        return deserialized.ToArray();
    }

    private void deleteFiles(IEnumerable<ITempLogFile> files)
    {
        foreach (var file in files)
        {
            file.Delete();
        }
    }
}
