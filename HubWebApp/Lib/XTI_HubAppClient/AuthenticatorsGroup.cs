// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthenticatorsGroup : AppClientGroup
{
    public AuthenticatorsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Authenticators")
    {
        Actions = new AuthenticatorsGroupActions(RegisterAuthenticator: CreatePostAction<RegisterAuthenticatorRequest, AuthenticatorModel>("RegisterAuthenticator"), RegisterUserAuthenticator: CreatePostAction<RegisterUserAuthenticatorRequest, AuthenticatorModel>("RegisterUserAuthenticator"));
    }

    public AuthenticatorsGroupActions Actions { get; }

    public Task<AuthenticatorModel> RegisterAuthenticator(RegisterAuthenticatorRequest model, CancellationToken ct = default) => Actions.RegisterAuthenticator.Post("", model, ct);
    public Task<AuthenticatorModel> RegisterUserAuthenticator(RegisterUserAuthenticatorRequest model, CancellationToken ct = default) => Actions.RegisterUserAuthenticator.Post("", model, ct);
    public sealed record AuthenticatorsGroupActions(AppClientPostAction<RegisterAuthenticatorRequest, AuthenticatorModel> RegisterAuthenticator, AppClientPostAction<RegisterUserAuthenticatorRequest, AuthenticatorModel> RegisterUserAuthenticator);
}