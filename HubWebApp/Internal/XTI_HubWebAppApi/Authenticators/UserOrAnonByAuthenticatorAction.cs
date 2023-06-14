namespace XTI_HubWebAppApi.Authenticators;

internal sealed class UserOrAnonByAuthenticatorAction : AppAction<UserOrAnonByAuthenticatorRequest, AppUserModel>
{
    private readonly HubFactory hubFactory;

    public UserOrAnonByAuthenticatorAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppUserModel> Execute(UserOrAnonByAuthenticatorRequest getRequest, CancellationToken stoppingToken)
    {
        var authenticatorKey = new AuthenticatorKey(getRequest.AuthenticatorKey);
        var user = await hubFactory.Users.UserOrAnonByExternalKey(authenticatorKey, getRequest.ExternalUserKey);
        return user.ToModel();
    }
}
