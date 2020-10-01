using XTI_App.Api;

namespace HubWebApp.AuthApi
{
    public interface IAuthGroupFactory
    {
        AppAction<LoginModel, LoginResult> CreateAuthenticateAction(AppApiUser user);
        AppAction<LoginModel, LoginResult> CreateLoginAction(AppApiUser user);
    }
}