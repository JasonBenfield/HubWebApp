namespace XTI_HubWebAppApiActions.System;

public sealed class GetUserAuthenticatorsAction : AppAction<AppUserIDRequest, UserAuthenticatorModel[]>
{
    private readonly HubFactory hubFactory;

    public GetUserAuthenticatorsAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<UserAuthenticatorModel[]> Execute(AppUserIDRequest getRequest, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.User(getRequest.UserID);
        var authenticators = await user.Authenticators();
        return authenticators;
    }
}
