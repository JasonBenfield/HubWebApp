using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.Auth;

public sealed class LogoutAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly ILogoutProcess logoutProcess;

    public LogoutAction(ILogoutProcess logoutProcess)
    {
        this.logoutProcess = logoutProcess;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest model)
    {
        await logoutProcess.Run();
        return new EmptyActionResult();
    }
}