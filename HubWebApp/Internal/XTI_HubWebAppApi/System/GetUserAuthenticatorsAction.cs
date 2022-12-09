namespace XTI_HubWebAppApi.System;

internal sealed class GetUserAuthenticatorsAction : AppAction<int, UserAuthenticatorModel[]>
{
    private readonly HubFactory hubFactory;

    public GetUserAuthenticatorsAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<UserAuthenticatorModel[]> Execute(int userID, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.User(userID);
        var authenticators = await user.Authenticators();
        return authenticators;
    }
}
