namespace XTI_HubWebAppApi.Auth;

public sealed class AuthApiGroup : AppApiGroupWrapper
{
    public AuthApiGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Authenticate = source.AddAction
        (
            nameof(Authenticate),
            () => sp.GetRequiredService<AuthenticateAction>(),
            () => new AuthenticateValidation()
        );
    }
    public AppApiAction<AuthenticateRequest, LoginResult> Authenticate { get; }
}