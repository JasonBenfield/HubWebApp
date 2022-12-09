namespace XTI_HubWebAppApi.System;

internal class GetUserOrAnonAction : AppAction<string, AppUserModel>
{
    private readonly HubFactory hubFactory;

    public GetUserOrAnonAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppUserModel> Execute(string userName, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.UserOrAnon(new AppUserName(userName));
        return user.ToModel();
    }
}
