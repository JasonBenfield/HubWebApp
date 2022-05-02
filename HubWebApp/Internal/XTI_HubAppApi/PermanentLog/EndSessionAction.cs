using XTI_TempLog.Abstractions;

namespace XTI_HubAppApi.PermanentLog;

public sealed class EndSessionAction : AppAction<EndSessionModel, EmptyActionResult>
{
    private readonly PermanentLog permanentLog;

    public EndSessionAction(PermanentLog permanentLog)
    {
        this.permanentLog = permanentLog;
    }

    public async Task<EmptyActionResult> Execute(EndSessionModel model)
    {
        await permanentLog.EndSession(model);
        return new EmptyActionResult();
    }
}