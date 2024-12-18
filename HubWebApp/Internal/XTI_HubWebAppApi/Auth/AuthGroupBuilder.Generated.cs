using XTI_HubWebAppApiActions.Auth;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Auth;
public sealed partial class AuthGroupBuilder
{
    private readonly AppApiGroup source;
    internal AuthGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        Login = source.AddAction<AuthenticatedLoginRequest, WebRedirectResult>("Login").WithExecution<LoginAction>().WithValidation<LoginValidation>();
        LoginReturnKey = source.AddAction<LoginReturnModel, string>("LoginReturnKey").WithExecution<LoginReturnKeyAction>();
        VerifyLogin = source.AddAction<VerifyLoginForm, AuthenticatedLoginResult>("VerifyLogin").WithExecution<VerifyLoginAction>();
        VerifyLoginForm = source.AddAction<EmptyRequest, WebPartialViewResult>("VerifyLoginForm").WithExecution<VerifyLoginFormAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<AuthenticatedLoginRequest, WebRedirectResult> Login { get; }
    public AppApiActionBuilder<LoginReturnModel, string> LoginReturnKey { get; }
    public AppApiActionBuilder<VerifyLoginForm, AuthenticatedLoginResult> VerifyLogin { get; }
    public AppApiActionBuilder<EmptyRequest, WebPartialViewResult> VerifyLoginForm { get; }

    public AuthGroup Build() => new AuthGroup(source, this);
}