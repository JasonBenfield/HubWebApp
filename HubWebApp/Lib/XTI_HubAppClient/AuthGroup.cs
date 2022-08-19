// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthGroup : AppClientGroup
{
    public AuthGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Auth")
    {
        Actions = new AuthGroupActions(VerifyLogin: CreatePostAction<VerifyLoginForm, string>("VerifyLogin"), VerifyLoginForm: CreateGetAction<EmptyRequest>("VerifyLoginForm"), Login: CreateGetAction<LoginModel>("Login"), LoginReturnKey: CreatePostAction<LoginReturnModel, string>("LoginReturnKey"));
    }

    public AuthGroupActions Actions { get; }

    public Task<string> VerifyLogin(VerifyLoginForm model, CancellationToken ct = default) => Actions.VerifyLogin.Post("", model, ct);
    public Task<string> LoginReturnKey(LoginReturnModel model, CancellationToken ct = default) => Actions.LoginReturnKey.Post("", model, ct);
    public sealed record AuthGroupActions(AppClientPostAction<VerifyLoginForm, string> VerifyLogin, AppClientGetAction<EmptyRequest> VerifyLoginForm, AppClientGetAction<LoginModel> Login, AppClientPostAction<LoginReturnModel, string> LoginReturnKey);
}