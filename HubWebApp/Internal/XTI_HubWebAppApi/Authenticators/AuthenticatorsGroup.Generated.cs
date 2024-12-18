using XTI_HubWebAppApiActions.Authenticators;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Authenticators;
public sealed partial class AuthenticatorsGroup : AppApiGroupWrapper
{
    internal AuthenticatorsGroup(AppApiGroup source, AuthenticatorsGroupBuilder builder) : base(source)
    {
        MoveAuthenticator = builder.MoveAuthenticator.Build();
        RegisterAuthenticator = builder.RegisterAuthenticator.Build();
        RegisterUserAuthenticator = builder.RegisterUserAuthenticator.Build();
        UserOrAnonByAuthenticator = builder.UserOrAnonByAuthenticator.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<MoveAuthenticatorRequest, EmptyActionResult> MoveAuthenticator { get; }
    public AppApiAction<RegisterAuthenticatorRequest, AuthenticatorModel> RegisterAuthenticator { get; }
    public AppApiAction<RegisterUserAuthenticatorRequest, AuthenticatorModel> RegisterUserAuthenticator { get; }
    public AppApiAction<UserOrAnonByAuthenticatorRequest, AppUserModel> UserOrAnonByAuthenticator { get; }
}