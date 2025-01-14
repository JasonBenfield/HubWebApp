// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthenticatorsGroup : AppClientGroup
{
    public AuthenticatorsGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Authenticators")
    {
        Actions = new AuthenticatorsGroupActions(MoveAuthenticator: CreatePostAction<MoveAuthenticatorRequest, EmptyActionResult>("MoveAuthenticator"), RegisterAuthenticator: CreatePostAction<RegisterAuthenticatorRequest, AuthenticatorModel>("RegisterAuthenticator"), RegisterUserAuthenticator: CreatePostAction<RegisterUserAuthenticatorRequest, AuthenticatorModel>("RegisterUserAuthenticator"), UserOrAnonByAuthenticator: CreatePostAction<UserOrAnonByAuthenticatorRequest, AppUserModel>("UserOrAnonByAuthenticator"));
        Configure();
    }

    partial void Configure();
    public AuthenticatorsGroupActions Actions { get; }

    public Task<EmptyActionResult> MoveAuthenticator(MoveAuthenticatorRequest requestData, CancellationToken ct = default) => Actions.MoveAuthenticator.Post("", requestData, ct);
    public Task<AuthenticatorModel> RegisterAuthenticator(RegisterAuthenticatorRequest requestData, CancellationToken ct = default) => Actions.RegisterAuthenticator.Post("", requestData, ct);
    public Task<AuthenticatorModel> RegisterUserAuthenticator(RegisterUserAuthenticatorRequest requestData, CancellationToken ct = default) => Actions.RegisterUserAuthenticator.Post("", requestData, ct);
    public Task<AppUserModel> UserOrAnonByAuthenticator(UserOrAnonByAuthenticatorRequest requestData, CancellationToken ct = default) => Actions.UserOrAnonByAuthenticator.Post("", requestData, ct);
    public sealed record AuthenticatorsGroupActions(AppClientPostAction<MoveAuthenticatorRequest, EmptyActionResult> MoveAuthenticator, AppClientPostAction<RegisterAuthenticatorRequest, AuthenticatorModel> RegisterAuthenticator, AppClientPostAction<RegisterUserAuthenticatorRequest, AuthenticatorModel> RegisterUserAuthenticator, AppClientPostAction<UserOrAnonByAuthenticatorRequest, AppUserModel> UserOrAnonByAuthenticator);
}