namespace XTI_HubWebAppApi.Authenticators;

public sealed class AuthenticatorsGroup : AppApiGroupWrapper
{
    public AuthenticatorsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        MoveAuthenticator = source.AddAction
        (
            nameof(MoveAuthenticator),
            () => sp.GetRequiredService<MoveAuthenticatorAction>()
        );
        RegisterAuthenticator = source.AddAction
        (
            nameof(RegisterAuthenticator),
            () => sp.GetRequiredService<RegisterAuthenticatorAction>()
        );
        RegisterUserAuthenticator = source.AddAction
        (
            nameof(RegisterUserAuthenticator),
            () => sp.GetRequiredService<RegisterUserAuthenticatorAction>()
        );
        UserOrAnonByAuthenticator = source.AddAction
        (
            nameof(UserOrAnonByAuthenticator),
            () => sp.GetRequiredService<UserOrAnonByAuthenticatorAction>()
        );
    }

    public AppApiAction<MoveAuthenticatorRequest, EmptyActionResult> MoveAuthenticator { get; }
    public AppApiAction<RegisterAuthenticatorRequest, AuthenticatorModel> RegisterAuthenticator { get; }
    public AppApiAction<RegisterUserAuthenticatorRequest, AuthenticatorModel> RegisterUserAuthenticator { get; }
    public AppApiAction<UserOrAnonByAuthenticatorRequest, AppUserModel> UserOrAnonByAuthenticator { get; }
}