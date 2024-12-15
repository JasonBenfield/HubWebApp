using XTI_HubWebAppApiActions.Auth;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Auth;
public sealed partial class AuthGroup : AppApiGroupWrapper
{
    internal AuthGroup(AppApiGroup source, AuthGroupBuilder builder) : base(source)
    {
        Login = builder.Login.Build();
        LoginReturnKey = builder.LoginReturnKey.Build();
        VerifyLogin = builder.VerifyLogin.Build();
        VerifyLoginForm = builder.VerifyLoginForm.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<AuthenticatedLoginRequest, WebRedirectResult> Login { get; }
    public AppApiAction<LoginReturnModel, string> LoginReturnKey { get; }
    public AppApiAction<VerifyLoginForm, AuthenticatedLoginResult> VerifyLogin { get; }
    public AppApiAction<EmptyRequest, WebPartialViewResult> VerifyLoginForm { get; }
}