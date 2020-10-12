using XTI_App.Api;
using System.Threading.Tasks;
using XTI_WebApp;

namespace HubWebApp.AuthApi
{
    public sealed class LoginAction : AppAction<LoginModel, LoginResult>
    {
        private readonly Authentication auth;
        private readonly IAnonClient anonClient;

        public LoginAction(Authentication auth, IAnonClient anonClient)
        {
            this.auth = auth;
            this.anonClient = anonClient;
        }

        public async Task<LoginResult> Execute(LoginModel model)
        {
            var result = await auth.Authenticate(model.UserName, model.Password);
            anonClient.Load();
            anonClient.Persist(0, anonClient.RequesterKey);
            return result;
        }
    }

}
