namespace XTI_HubWebAppApiActions.Authenticators;

public sealed class RegisterAuthenticatorAction : AppAction<RegisterAuthenticatorRequest, AuthenticatorModel>
{
    private readonly HubFactory hubFactory;

    public RegisterAuthenticatorAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AuthenticatorModel> Execute(RegisterAuthenticatorRequest registerRequest, CancellationToken stoppingToken)
    {
        var authenticatorKey = new AuthenticatorKey(registerRequest.AuthenticatorName);
        var authenticator = await hubFactory.Authenticators.AddOrUpdate(authenticatorKey);
        return authenticator;
    }
}