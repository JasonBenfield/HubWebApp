namespace XTI_HubAppApi.Auth;

public sealed class AuthGroup : AppApiGroupWrapper
{
    public AuthGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        VerifyLogin = source.AddAction
        (
            actions.Action
            (
                nameof(VerifyLogin),
                () => sp.GetRequiredService<VerifyLoginAction>()
            )
        );
        VerifyLoginForm = source.AddAction
        (
            actions.PartialView
            (
                nameof(VerifyLoginForm),
                () => sp.GetRequiredService<VerifyLoginFormAction>()
            )
        );
        Login = source.AddAction
        (
            actions.Action
            (
                nameof(Login),
                () => sp.GetRequiredService<LoginAction>()
            )
        );
        LoginReturnKey = source.AddAction
        (
            actions.Action
            (
                nameof(LoginReturnKey),
                ResourceAccess.AllowAuthenticated()
                    .WithAllowed(HubInfo.Roles.Admin, AppRoleName.System),
                () => sp.GetRequiredService<LoginReturnKeyAction>()
            )
        );

    }
    public AppApiAction<VerifyLoginForm, string> VerifyLogin { get; }
    public AppApiAction<EmptyRequest, WebPartialViewResult> VerifyLoginForm { get; }
    public AppApiAction<LoginModel, WebRedirectResult> Login { get; }
    public AppApiAction<LoginReturnModel, string> LoginReturnKey { get; }
}