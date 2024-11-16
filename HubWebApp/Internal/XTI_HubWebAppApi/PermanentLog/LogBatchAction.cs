using XTI_TempLog.Abstractions;

namespace XTI_HubWebAppApi.PermanentLog;

public sealed class LogBatchAction : AppAction<LogBatchModel, EmptyActionResult>
{
    private readonly EfPermanentLog permanentLog;

    public LogBatchAction(EfPermanentLog permanentLog)
    {
        this.permanentLog = permanentLog;
    }

    public async Task<EmptyActionResult> Execute(LogBatchModel model, CancellationToken stoppingToken)
    {
        await permanentLog.LogBatch(model, stoppingToken);
        return new EmptyActionResult();
    }
}