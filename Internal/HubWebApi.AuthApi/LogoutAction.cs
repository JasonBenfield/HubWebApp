using System.Threading.Tasks;
using XTI_App.Api;

namespace HubWebApp.AuthApi
{
    public sealed class LogoutAction : AppAction<EmptyRequest, AppActionRedirectResult>
    {
        private readonly AccessForLogin access;

        public LogoutAction(AccessForLogin access)
        {
            this.access = access;
        }

        public async Task<AppActionRedirectResult> Execute(EmptyRequest model)
        {
            await access.Logout();
            return new AppActionRedirectResult("~/Auth");
        }
    }
}
