using XTI_Core;
using XTI_TempLog;

namespace XTI_SupportServiceAppApi.PermanentLog;

public sealed class RetryV1Action : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly ITempLogsV1 tempLogs;
    private readonly IClock clock;

    public RetryV1Action(ITempLogsV1 tempLogs, IClock clock)
    {
        this.tempLogs = tempLogs;
        this.clock = clock;
    }

    public Task<bool> IsOptional()
    {
        var filesInProgress = GetFilesInProgressV1();
        var isOptional = !filesInProgress.Any();
        return Task.FromResult(isOptional);
    }

    private static readonly string processingExtension = ".processing";

    public Task<EmptyActionResult> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var filesInProgressV1 = GetFilesInProgressV1();
        foreach (var file in filesInProgressV1)
        {
            file.WithNewName(file.Name.Remove(file.Name.Length - processingExtension.Length));
        }
        return Task.FromResult(new EmptyActionResult());
    }

    private IEnumerable<ITempLogFileV1> GetFilesInProgressV1()
    {
        var modifiedBefore = clock.Now().AddMinutes(-1);
        var logs = tempLogs.Logs();
        return logs.SelectMany(l => l.ProcessingFiles(modifiedBefore)).ToArray();
    }
}
