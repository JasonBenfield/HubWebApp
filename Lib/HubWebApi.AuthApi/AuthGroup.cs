using XTI_App.Api;

namespace HubWebApp.AuthApi
{
    public sealed class AuthGroup : AppApiGroup
    {
        public AuthGroup(AppApi api, IAuthGroupFactory factory)
            : base
            (
                  api,
                  new NameFromGroupClassName(nameof(AuthGroup)).Value,
                  false,
                  ResourceAccess.AllowAnonymous(),
                  new AppApiSuperUser()
            )
        {
            Index = AddDefaultView();
            Start = AddAction
            (
               nameof(Start),
                (u) => new AppActionValidationNotRequired<StartRequest>(),
                u => new StartAction()
            );
            Login = AddAction
            (
                nameof(Login),
                (u) => new LoginValidation(),
                factory.CreateLoginAction
            );
            Authenticate = AddAction
            (
                nameof(Authenticate),
                (u) => new LoginValidation(),
                factory.CreateAuthenticateAction
            );
        }
        public AppApiAction<EmptyRequest, AppActionViewResult> Index { get; }
        public AppApiAction<StartRequest, AppActionRedirectResult> Start { get; }
        public AppApiAction<LoginModel, LoginResult> Login { get; }
        public AppApiAction<LoginModel, LoginResult> Authenticate { get; }
    }
}
