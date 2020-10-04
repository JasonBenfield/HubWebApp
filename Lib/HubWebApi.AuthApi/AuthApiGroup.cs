using XTI_App.Api;

namespace HubWebApp.AuthApi
{
    public sealed class AuthApiGroup : AppApiGroup
    {
        public AuthApiGroup(AppApi api, IAuthGroupFactory factory)
            : base
            (
                  api,
                  new NameFromGroupClassName(nameof(AuthApiGroup)).Value,
                  false,
                  ResourceAccess.AllowAnonymous(),
                  new AppApiSuperUser()
            )
        {
            Authenticate = AddAction
            (
                nameof(Authenticate),
                (u) => new LoginValidation(),
                factory.CreateAuthenticateAction
            );
        }
        public AppApiAction<LoginModel, LoginResult> Authenticate { get; }
    }
}
