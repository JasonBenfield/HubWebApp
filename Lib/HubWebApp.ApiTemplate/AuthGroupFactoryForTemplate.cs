using HubWebApp.AuthApi;
using XTI_WebApp.Api;

namespace HubWebApp.ApiTemplate
{
    internal class AuthGroupFactoryForTemplate : IAuthGroupFactory
    {
        public AppAction<LoginModel, LoginResult> CreateAuthenticateAction(WebAppUser user) => new EmptyAppAction<LoginModel, LoginResult>();
        public AppAction<LoginModel, LoginResult> CreateLoginAction(WebAppUser user) => new EmptyAppAction<LoginModel, LoginResult>();
    }
}
