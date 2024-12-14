namespace XTI_HubWebAppApiActions.System;

public class GetUserOrAnonAction : AppAction<AppUserNameRequest, AppUserModel>
{
    private readonly HubFactory hubFactory;

    public GetUserOrAnonAction(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task<AppUserModel> Execute(AppUserNameRequest getRequest, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.UserOrAnon(getRequest.ToAppUserName());
        return user.ToModel();
    }
}
