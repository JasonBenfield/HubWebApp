// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthenticatorsGroup : AppClientGroup
{
    public AuthenticatorsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Authenticators")
    {
        Actions = new AuthenticatorsGroupActions(MoveAuthenticator: CreatePostAction<MoveAuthenticatorRequest, EmptyActionResult>("MoveAuthenticator"), RegisterAuthenticator: CreatePostAction<RegisterAuthenticatorRequest, AuthenticatorModel>("RegisterAuthenticator"), RegisterUserAuthenticator: CreatePostAction<RegisterUserAuthenticatorRequest, AuthenticatorModel>("RegisterUserAuthenticator"), UserOrAnonByAuthenticator: CreatePostAction<UserOrAnonByAuthenticatorRequest, AppUserModel>("UserOrAnonByAuthenticator"));
    }

    public AuthenticatorsGroupActions Actions { get; }

    public Task<EmptyActionResult> MoveAuthenticator(MoveAuthenticatorRequest model, CancellationToken ct = default) => Actions.MoveAuthenticator.Post("", model, ct);
    public Task<AuthenticatorModel> RegisterAuthenticator(RegisterAuthenticatorRequest model, CancellationToken ct = default) => Actions.RegisterAuthenticator.Post("", model, ct);
    public Task<AuthenticatorModel> RegisterUserAuthenticator(RegisterUserAuthenticatorRequest model, CancellationToken ct = default) => Actions.RegisterUserAuthenticator.Post("", model, ct);
    public Task<AppUserModel> UserOrAnonByAuthenticator(UserOrAnonByAuthenticatorRequest model, CancellationToken ct = default) => Actions.UserOrAnonByAuthenticator.Post("", model, ct);
    public sealed record AuthenticatorsGroupActions(AppClientPostAction<MoveAuthenticatorRequest, EmptyActionResult> MoveAuthenticator, AppClientPostAction<RegisterAuthenticatorRequest, AuthenticatorModel> RegisterAuthenticator, AppClientPostAction<RegisterUserAuthenticatorRequest, AuthenticatorModel> RegisterUserAuthenticator, AppClientPostAction<UserOrAnonByAuthenticatorRequest, AppUserModel> UserOrAnonByAuthenticator);
}