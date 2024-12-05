using System.Text.Json;
using XTI_Core;
using XTI_TempLog;
using XTI_TempLog.Abstractions;

namespace XTI_PermanentLog;

public sealed class TempToPermanentLogV1
{
    private readonly ITempLogsV1 tempLogs;
    private readonly IPermanentLog permanentLog;
    private readonly IClock clock;
    private readonly int processMinutesBefore = 1;
    private readonly List<ITempLogFileV1> filesInProgress = new();

    public TempToPermanentLogV1(ITempLogsV1 tempLogs, IPermanentLog permanentLog, IClock clock, int processMinutesBefore = 1)
    {
        this.tempLogs = tempLogs;
        this.permanentLog = permanentLog;
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
            await permanentLog.LogBatch(logBatch, default);
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

    private async Task<LogBatchModel> processBatch(IEnumerable<TempLogV1> logs, DateTimeOffset modifiedBefore)
    {
        var logBatch = new LogBatchModel();
        var startSessionFiles = startProcessingFiles(logs, l => l.StartSessionFiles(modifiedBefore));
        logBatch.StartSessions = await deserializeFiles<StartSessionModel>(startSessionFiles);
        var startRequestFiles = startProcessingFiles(logs, l => l.StartRequestFiles(modifiedBefore));
        logBatch.StartRequests = await deserializeFiles<StartRequestModel>(startRequestFiles);
        var authSessionFiles = startProcessingFiles(logs, l => l.AuthSessionFiles(modifiedBefore));
        logBatch.AuthenticateSessions = await deserializeFiles<AuthenticateSessionModel>(authSessionFiles);
        var logEventFiles = startProcessingFiles(logs, l => l.LogEventFiles(modifiedBefore));
        logBatch.LogEntries = await deserializeFiles<LogEntryModelV1>(logEventFiles);
        var endRequestFiles = startProcessingFiles(logs, l => l.EndRequestFiles(modifiedBefore));
        logBatch.EndRequests = await deserializeFiles<EndRequestModel>(endRequestFiles);
        var endSessionFiles = startProcessingFiles(logs, l => l.EndSessionFiles(modifiedBefore));
        logBatch.EndSessions = await deserializeFiles<EndSessionModel>(endSessionFiles);
        return logBatch;
    }

    private IEnumerable<ITempLogFileV1> startProcessingFiles(IEnumerable<TempLogV1> logs, Func<TempLogV1, IEnumerable<ITempLogFileV1>> getFiles)
    {
        var filesToProcess = logs
            .SelectMany(l => getFiles(l))
            .Take(50);
        var renamedFiles = new List<ITempLogFileV1>();
        foreach(var fileToProcess in filesToProcess)
        {
            try
            {
                var renamedFile = fileToProcess.WithNewName($"{fileToProcess.Name}.processing");
                renamedFiles.Add(renamedFile);
                filesInProgress.Add(renamedFile);
            }
            catch { }
        }
        return renamedFiles;
    }

    private async Task<T[]> deserializeFiles<T>(IEnumerable<ITempLogFileV1> files)
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

    private void deleteFiles(IEnumerable<ITempLogFileV1> files)
    {
        foreach (var file in files)
        {
            try
            {
                file.Delete();
            }
            catch { }
        }
    }
}
