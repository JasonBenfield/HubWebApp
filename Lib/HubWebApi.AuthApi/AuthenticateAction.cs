using XTI_App.Api;
using System.Threading.Tasks;

namespace HubWebApp.AuthApi
{
    public sealed class AuthenticateAction : AppAction<LoginModel, LoginResult>
    {
        public AuthenticateAction(Authentication auth)
        {
            this.auth = auth;
        }

        private readonly Authentication auth;

        public Task<LoginResult> Execute(LoginModel model) => auth.Authenticate(model.UserName, model.Password);
    }

}
