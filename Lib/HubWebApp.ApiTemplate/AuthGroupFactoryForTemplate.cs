using HubWebApp.AuthApi;
using XTI_App.Api;

namespace HubWebApp.ApiTemplate
{
    internal class AuthGroupFactoryForTemplate : IAuthGroupFactory
    {
        public AppAction<LoginModel, LoginResult> CreateAuthenticateAction(IAppApiUser user) => new EmptyAppAction<LoginModel, LoginResult>();
        public AppAction<LoginModel, LoginResult> CreateLoginAction(IAppApiUser user) => new EmptyAppAction<LoginModel, LoginResult>();
    }
}
