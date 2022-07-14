namespace XTI_HubWebAppApi.Authenticators;

public sealed class AuthenticatorsGroup : AppApiGroupWrapper
{
    public AuthenticatorsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
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
    }

    public AppApiAction<EmptyRequest, EmptyActionResult> RegisterAuthenticator { get; }
    public AppApiAction<RegisterUserAuthenticatorRequest, EmptyActionResult> RegisterUserAuthenticator { get; }
}