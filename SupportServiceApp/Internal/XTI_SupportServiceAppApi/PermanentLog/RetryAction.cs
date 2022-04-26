using XTI_Core;
using XTI_TempLog;

namespace XTI_SupportServiceAppApi.PermanentLog;

internal sealed class RetryAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly ITempLogs tempLogs;
    private readonly IClock clock;

    public RetryAction(ITempLogs tempLogs, IClock clock)
    {
        this.tempLogs = tempLogs;
        this.clock = clock;
    }

    public Task<bool> IsOptional()
    {
        var filesInProgress = getFilesInProgress();
        var isOptional = !filesInProgress.Any();
        return Task.FromResult(isOptional);
    }

    private static readonly string processingExtension = ".processing";

    public Task<EmptyActionResult> Execute(EmptyRequest model)
    {
        var filesInProgress = getFilesInProgress();
        foreach (var file in filesInProgress)
        {
            file.WithNewName(file.Name.Remove(file.Name.Length - processingExtension.Length));
        }
        return Task.FromResult(new EmptyActionResult());
    }

    private IEnumerable<ITempLogFile> getFilesInProgress()
    {
        var modifiedBefore = clock.Now().AddMinutes(-1);
        var logs = tempLogs.Logs();
        return logs.SelectMany(l => l.ProcessingFiles(modifiedBefore)).ToArray();
    }
}
