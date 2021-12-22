using XTI_App.Api;
using XTI_TempLog;

namespace XTI_HubAppApi.PermanentLog;

public sealed class StartSessionAction : AppAction<StartSessionModel, EmptyActionResult>
{
    private readonly PermanentLog permanentLog;

    public StartSessionAction(PermanentLog permanentLog)
    {
        this.permanentLog = permanentLog;
    }

    public async Task<EmptyActionResult> Execute(StartSessionModel model)
    {
        await permanentLog.StartSession(model);
        return new EmptyActionResult();
    }
}