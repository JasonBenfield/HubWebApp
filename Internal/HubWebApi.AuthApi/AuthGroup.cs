using XTI_App.Api;

namespace HubWebApp.AuthApi
{
    public sealed class AuthGroup : AppApiGroup
    {
        public AuthGroup(AppApi api, AuthGroupFactory factory)
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
                () => new AppActionValidationNotRequired<StartRequest>(),
                () => new StartAction()
            );
            Login = AddAction
            (
                nameof(Login),
                () => new LoginValidation(),
                factory.CreateLoginAction
            );
            Logout = AddAction
            (
                nameof(Logout),
                () => factory.CreateLogoutAction()
            );
        }
        public AppApiAction<EmptyRequest, AppActionViewResult> Index { get; }
        public AppApiAction<StartRequest, AppActionRedirectResult> Start { get; }
        public AppApiAction<LoginModel, LoginResult> Login { get; }
        public AppApiAction<EmptyRequest, AppActionRedirectResult> Logout { get; }
    }
}
