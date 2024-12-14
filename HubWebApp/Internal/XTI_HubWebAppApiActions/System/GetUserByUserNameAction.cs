namespace XTI_HubWebAppApiActions.System;

public class GetUserByUserNameAction : AppAction<AppUserNameRequest, AppUserModel>
{
    private readonly HubFactory hubFactory;

    public GetUserByUserNameAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppUserModel> Execute(AppUserNameRequest getRequest, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.UserByUserName(getRequest.ToAppUserName());
        return user.ToModel();
    }
}
