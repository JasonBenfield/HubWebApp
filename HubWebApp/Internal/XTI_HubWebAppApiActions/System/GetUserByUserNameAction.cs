namespace XTI_HubWebAppApiActions.System;

public class GetUserByUserNameAction : AppAction<AppUserNameRequest, AppUserModel>
{
    private readonly EfHubDB hubFactory;

    public GetUserByUserNameAction(EfHubDB hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppUserModel> Execute(AppUserNameRequest getRequest, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.UserByUserName(getRequest.ToAppUserName(), stoppingToken);
        return user.ToModel();
    }
}
