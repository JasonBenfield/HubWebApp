using XTI_WebApp.Api;

namespace HubWebApp.AuthApi
{
    public interface IAuthGroupFactory
    {
        AppAction<LoginModel, LoginResult> CreateAuthenticateAction(WebAppUser user);
        AppAction<LoginModel, LoginResult> CreateLoginAction(WebAppUser user);
    }
}