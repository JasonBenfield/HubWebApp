using XTI_App.Api;
using XTI_TempLog.Abstractions;

namespace XTI_HubAppApi.PermanentLog;

public sealed class AuthenticateSessionAction : AppAction<AuthenticateSessionModel, EmptyActionResult>
{
    private readonly PermanentLog permanentLog;

    public AuthenticateSessionAction(PermanentLog permanentLog)
    {
        this.permanentLog = permanentLog;
    }

    public async Task<EmptyActionResult> Execute(AuthenticateSessionModel model)
    {
        await permanentLog.AuthenticateSession(model);
        return new EmptyActionResult();
    }
}