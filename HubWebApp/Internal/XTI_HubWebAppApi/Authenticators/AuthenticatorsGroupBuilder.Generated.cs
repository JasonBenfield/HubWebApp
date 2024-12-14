using XTI_HubWebAppApiActions.Authenticators;

// Generated Code
namespace XTI_HubWebAppApi.Authenticators;
public sealed partial class AuthenticatorsGroupBuilder
{
    private readonly AppApiGroup source;
    internal AuthenticatorsGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        MoveAuthenticator = source.AddAction<MoveAuthenticatorRequest, EmptyActionResult>("MoveAuthenticator").WithExecution<MoveAuthenticatorAction>();
        RegisterAuthenticator = source.AddAction<RegisterAuthenticatorRequest, AuthenticatorModel>("RegisterAuthenticator").WithExecution<RegisterAuthenticatorAction>();
        RegisterUserAuthenticator = source.AddAction<RegisterUserAuthenticatorRequest, AuthenticatorModel>("RegisterUserAuthenticator").WithExecution<RegisterUserAuthenticatorAction>();
        UserOrAnonByAuthenticator = source.AddAction<UserOrAnonByAuthenticatorRequest, AppUserModel>("UserOrAnonByAuthenticator").WithExecution<UserOrAnonByAuthenticatorAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<MoveAuthenticatorRequest, EmptyActionResult> MoveAuthenticator { get; }
    public AppApiActionBuilder<RegisterAuthenticatorRequest, AuthenticatorModel> RegisterAuthenticator { get; }
    public AppApiActionBuilder<RegisterUserAuthenticatorRequest, AuthenticatorModel> RegisterUserAuthenticator { get; }
    public AppApiActionBuilder<UserOrAnonByAuthenticatorRequest, AppUserModel> UserOrAnonByAuthenticator { get; }

    public AuthenticatorsGroup Build() => new AuthenticatorsGroup(source, this);
}