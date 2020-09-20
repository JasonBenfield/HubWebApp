using XTI_WebApp.Api;
using System.Threading.Tasks;

namespace HubWebApp.AuthApi
{
    public sealed class LoginAction : AppAction<LoginModel, LoginResult>
    {
        public LoginAction(Authentication auth)
        {
            this.auth = auth;
        }

        private readonly Authentication auth;

        public async Task<LoginResult> Execute(LoginModel model)
        {
            var result = await auth.Authenticate(model.UserName, model.Password);
            return result;
        }
    }

}
