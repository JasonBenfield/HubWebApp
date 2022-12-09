namespace XTI_HubWebAppApi.Authenticators;

internal sealed class RegisterAuthenticatorAction : AppAction<RegisterAuthenticatorRequest, AuthenticatorModel>
{
    private readonly HubFactory hubFactory;

    public RegisterAuthenticatorAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AuthenticatorModel> Execute(RegisterAuthenticatorRequest model, CancellationToken stoppingToken)
    {
        var authenticatorKey = new AuthenticatorKey(model.AuthenticatorName);
        var authenticator = await hubFactory.Authenticators.AddOrUpdate(authenticatorKey);
        return authenticator;
    }
}