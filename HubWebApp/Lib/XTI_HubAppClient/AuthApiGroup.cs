// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthApiGroup : AppClientGroup
{
    public AuthApiGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "AuthApi")
    {
        Actions = new AuthApiGroupActions(Authenticate: CreatePostAction<AuthenticateRequest, LoginResult>("Authenticate"));
    }

    public AuthApiGroupActions Actions { get; }

    public Task<LoginResult> Authenticate(AuthenticateRequest model, CancellationToken ct = default) => Actions.Authenticate.Post("", model, ct);
    public sealed record AuthApiGroupActions(AppClientPostAction<AuthenticateRequest, LoginResult> Authenticate);
}