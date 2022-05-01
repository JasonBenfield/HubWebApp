using XTI_TempLog.Abstractions;

namespace XTI_HubAppApi.PermanentLog;

public sealed class LogEventAction : AppAction<LogEventModel, EmptyActionResult>
{
    private readonly PermanentLog permanentLog;

    public LogEventAction(PermanentLog permanentLog)
    {
        this.permanentLog = permanentLog;
    }

    public async Task<EmptyActionResult> Execute(LogEventModel model)
    {
        await permanentLog.LogEvent(model);
        return new EmptyActionResult();
    }
}