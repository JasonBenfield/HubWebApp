namespace XTI_HubWebAppApiActions.System;

public class GetUserOrAnonAction : AppAction<AppUserNameRequest, AppUserModel>
{
    private readonly EfHubDB hubFactory;

    public GetUserOrAnonAction(EfHubDB hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppUserModel> Execute(AppUserNameRequest getRequest, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.UserOrAnon(getRequest.ToAppUserName(), stoppingToken);
        return user.ToModel();
    }
}
