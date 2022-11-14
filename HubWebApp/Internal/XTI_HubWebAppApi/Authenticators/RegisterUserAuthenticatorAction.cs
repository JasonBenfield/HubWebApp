namespace XTI_HubWebAppApi.Authenticators;

internal sealed class RegisterUserAuthenticatorAction : AppAction<RegisterUserAuthenticatorRequest, AuthenticatorModel>
{
    private readonly HubFactory hubFactory;

    public RegisterUserAuthenticatorAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AuthenticatorModel> Execute(RegisterUserAuthenticatorRequest model, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.User(model.UserID);
        var authenticatorKey = new AuthenticatorKey(model.AuthenticatorKey);
        var authenticator = await user.AddAuthenticator(authenticatorKey, model.ExternalUserKey);
        return authenticator;
    }
}