using HubWebApp.AuthApi;
using XTI_App.Api;

namespace HubWebApp.ApiTemplate
{
    internal class AuthGroupFactoryForTemplate : IAuthGroupFactory
    {
        public AppAction<LoginModel, LoginResult> CreateAuthenticateAction(AppApiUser user) => new EmptyAppAction<LoginModel, LoginResult>();
        public AppAction<LoginModel, LoginResult> CreateLoginAction(AppApiUser user) => new EmptyAppAction<LoginModel, LoginResult>();
    }
}
