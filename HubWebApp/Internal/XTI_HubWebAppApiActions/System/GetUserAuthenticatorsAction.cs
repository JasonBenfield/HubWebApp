namespace XTI_HubWebAppApiActions.System;

public sealed class GetUserAuthenticatorsAction : AppAction<AppUserIDRequest, UserAuthenticatorModel[]>
{
    private readonly EfHubDB hubFactory;

    public GetUserAuthenticatorsAction(EfHubDB hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<UserAuthenticatorModel[]> Execute(AppUserIDRequest getRequest, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.User(getRequest.UserID, stoppingToken);
        var authenticators = await user.Authenticators(stoppingToken);
        return authenticators;
    }
}
