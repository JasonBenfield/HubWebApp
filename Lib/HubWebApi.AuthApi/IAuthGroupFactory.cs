using XTI_App.Api;

namespace HubWebApp.AuthApi
{
    public interface IAuthGroupFactory
    {
        AppAction<LoginModel, LoginResult> CreateAuthenticateAction(IAppApiUser user);
        AppAction<LoginModel, LoginResult> CreateLoginAction(IAppApiUser user);
    }
}