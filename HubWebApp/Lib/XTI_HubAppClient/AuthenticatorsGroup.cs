// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthenticatorsGroup : AppClientGroup
{
    public AuthenticatorsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Authenticators")
    {
        Actions = new AuthenticatorsGroupActions(RegisterAuthenticator: CreatePostAction<EmptyRequest, EmptyActionResult>("RegisterAuthenticator"), RegisterUserAuthenticator: CreatePostAction<RegisterUserAuthenticatorRequest, EmptyActionResult>("RegisterUserAuthenticator"));
    }

    public AuthenticatorsGroupActions Actions { get; }

    public Task<EmptyActionResult> RegisterAuthenticator(string modifier) => Actions.RegisterAuthenticator.Post(modifier, new EmptyRequest());
    public Task<EmptyActionResult> RegisterUserAuthenticator(string modifier, RegisterUserAuthenticatorRequest model) => Actions.RegisterUserAuthenticator.Post(modifier, model);
    public sealed record AuthenticatorsGroupActions(AppClientPostAction<EmptyRequest, EmptyActionResult> RegisterAuthenticator, AppClientPostAction<RegisterUserAuthenticatorRequest, EmptyActionResult> RegisterUserAuthenticator);
}