using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using XTI_TempLog;
using XTI_WebApp.Api;

namespace XTI_HubAppClient.WebApp.Extensions;

internal sealed class LogoutProcess : ILogoutProcess
{
    private readonly TempLogSession tempLogSession;
    private readonly IHttpContextAccessor httpContextAccessor;

    public LogoutProcess(TempLogSession tempLogSession, IHttpContextAccessor httpContextAccessor)
    {
        this.tempLogSession = tempLogSession;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task Run()
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            await httpContext.SignOutAsync();
        }
        await tempLogSession.EndSession();
    }
}
