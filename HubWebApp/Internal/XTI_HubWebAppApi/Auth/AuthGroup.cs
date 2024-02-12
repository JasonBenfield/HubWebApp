namespace XTI_HubWebAppApi.Auth;

public sealed class AuthGroup : AppApiGroupWrapper
{
    public AuthGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        VerifyLogin = source.AddAction
        (
            nameof(VerifyLogin),
            () => sp.GetRequiredService<VerifyLoginAction>()
        );
        VerifyLoginForm = source.AddAction
        (
            nameof(VerifyLoginForm),
            () => sp.GetRequiredService<VerifyLoginFormAction>()
        );
        Login = source.AddAction
        (
            nameof(Login),
            () => sp.GetRequiredService<LoginAction>(),
            () => sp.GetRequiredService<LoginValidation>()
        );
        LoginReturnKey = source.AddAction
        (
            nameof(LoginReturnKey),
            () => sp.GetRequiredService<LoginReturnKeyAction>(),
            access: ResourceAccess.AllowAuthenticated()
                .WithAllowed(HubInfo.Roles.Admin, AppRoleName.System)
        );

    }
    public AppApiAction<VerifyLoginForm, AuthenticatedLoginResult> VerifyLogin { get; }
    public AppApiAction<EmptyRequest, WebPartialViewResult> VerifyLoginForm { get; }
    public AppApiAction<AuthenticatedLoginRequest, WebRedirectResult> Login { get; }
    public AppApiAction<LoginReturnModel, string> LoginReturnKey { get; }
}