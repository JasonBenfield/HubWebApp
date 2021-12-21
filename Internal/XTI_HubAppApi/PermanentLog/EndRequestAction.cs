using XTI_App.Api;
using XTI_TempLog;

namespace XTI_HubAppApi.PermanentLog;

public sealed class EndRequestAction : AppAction<EndRequestModel, EmptyActionResult>
{
    private readonly PermanentLog permanentLog;

    public EndRequestAction(PermanentLog permanentLog)
    {
        this.permanentLog = permanentLog;
    }

    public async Task<EmptyActionResult> Execute(EndRequestModel model)
    {
        await permanentLog.EndRequest(model);
        return new EmptyActionResult();
    }
}