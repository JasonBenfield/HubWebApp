using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using XTI_App.Api;
using XTI_TempLog;
using XTI_WebApp.Api;

namespace XTI_HubAppApi
{
    public sealed class LogoutAction : AppAction<EmptyRequest, WebRedirectResult>
    {
        private readonly AccessForLogin access;
        private readonly TempLogSession tempLogSession;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LogoutAction(AccessForLogin access, TempLogSession tempLogSession, IHttpContextAccessor httpContextAccessor)
        {
            this.access = access;
            this.tempLogSession = tempLogSession;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<WebRedirectResult> Execute(EmptyRequest model)
        {
            await access.Logout();
            await tempLogSession.EndSession();
            var authUrl = "/Hub/Current/Auth";
            var query = httpContextAccessor.HttpContext?.Request?.QueryString.Value;
            if (!string.IsNullOrWhiteSpace(query))
            {
                authUrl += query;
            }
            return new WebRedirectResult(authUrl);
        }
    }
}
