using XTI_Core;
using XTI_TempLog;

namespace XTI_SupportServiceAppApi.PermanentLog;

internal sealed class RetryAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly TempLog tempLog;
    private readonly IClock clock;

    public RetryAction(TempLog tempLog, IClock clock)
    {
        this.tempLog = tempLog;
        this.clock = clock;
    }

    public Task<bool> IsOptional()
    {
        var filesInProgress = GetFilesInProgress();
        var isOptional = !filesInProgress.Any();
        return Task.FromResult(isOptional);
    }

    private static readonly string processingExtension = ".processing";

    public Task<EmptyActionResult> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var filesInProgress = GetFilesInProgress();
        foreach (var file in filesInProgress)
        {
            file.WithNewName(file.Name.Remove(file.Name.Length - processingExtension.Length));
        }
        return Task.FromResult(new EmptyActionResult());
    }

    private ITempLogFile[] GetFilesInProgress()
    {
        var modifiedBefore = clock.Now().AddMinutes(-1);
        return tempLog.Files(modifiedBefore);
    }
}
