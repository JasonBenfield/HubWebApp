// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthGroup : AppClientGroup
{
    public AuthGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Auth")
    {
        Actions = new AuthGroupActions(Login: CreateGetAction<AuthenticatedLoginRequest>("Login"), LoginReturnKey: CreatePostAction<LoginReturnModel, string>("LoginReturnKey"), VerifyLogin: CreatePostAction<VerifyLoginForm, AuthenticatedLoginResult>("VerifyLogin"), VerifyLoginForm: CreateGetAction<EmptyRequest>("VerifyLoginForm"));
    }

    public AuthGroupActions Actions { get; }

    public Task<string> LoginReturnKey(LoginReturnModel model, CancellationToken ct = default) => Actions.LoginReturnKey.Post("", model, ct);
    public Task<AuthenticatedLoginResult> VerifyLogin(VerifyLoginForm model, CancellationToken ct = default) => Actions.VerifyLogin.Post("", model, ct);
    public sealed record AuthGroupActions(AppClientGetAction<AuthenticatedLoginRequest> Login, AppClientPostAction<LoginReturnModel, string> LoginReturnKey, AppClientPostAction<VerifyLoginForm, AuthenticatedLoginResult> VerifyLogin, AppClientGetAction<EmptyRequest> VerifyLoginForm);
}