using XTI_TempLog.Abstractions;

namespace XTI_HubWebAppApiActions.PermanentLog;

public sealed class LogSessionDetailsAction : AppAction<LogSessionDetailsRequest, EmptyActionResult>
{
    private readonly EfPermanentLog permanentLog;

    public LogSessionDetailsAction(EfPermanentLog permanentLog)
    {
        this.permanentLog = permanentLog;
    }

    public async Task<EmptyActionResult> Execute(LogSessionDetailsRequest logRequest, CancellationToken stoppingToken)
    {
        await permanentLog.LogSessionDetails(logRequest.SessionDetails, stoppingToken);
        return new EmptyActionResult();
    }
}
