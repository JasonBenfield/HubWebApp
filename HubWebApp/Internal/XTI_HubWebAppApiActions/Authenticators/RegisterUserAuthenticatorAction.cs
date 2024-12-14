namespace XTI_HubWebAppApiActions.Authenticators;

public sealed class RegisterUserAuthenticatorAction : AppAction<RegisterUserAuthenticatorRequest, AuthenticatorModel>
{
    private readonly HubFactory hubFactory;

    public RegisterUserAuthenticatorAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AuthenticatorModel> Execute(RegisterUserAuthenticatorRequest registerRequest, CancellationToken stoppingToken)
    {
        var authenticatorKey = new AuthenticatorKey(registerRequest.AuthenticatorKey);
        var existingUser = await hubFactory.Users.UserOrAnonByExternalKey(authenticatorKey, registerRequest.ExternalUserKey);
        if (!existingUser.IsUserName(AppUserName.Anon) && !existingUser.HasID(registerRequest.UserID))
        {
            throw new AppException
            (
                string.Format
                (
                    AppErrors.AuthenticatorExistsForDifferentUser, 
                    authenticatorKey.DisplayText,
                    registerRequest.ExternalUserKey,
                    existingUser.ToModel().ID
                ),
                $"User already exists with external user key '{registerRequest.ExternalUserKey}'"
            );
        }
        var user = await hubFactory.Users.User(registerRequest.UserID);
        var authenticator = await user.AddAuthenticator(authenticatorKey, registerRequest.ExternalUserKey);
        return authenticator;
    }
}