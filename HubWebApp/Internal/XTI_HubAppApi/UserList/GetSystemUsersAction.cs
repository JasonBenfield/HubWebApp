namespace XTI_HubAppApi.UserList;

public sealed class GetSystemUsersAction : AppAction<AppKey, AppUserModel[]>
{
    private readonly HubFactory appFactory;

    public GetSystemUsersAction(HubFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<AppUserModel[]> Execute(AppKey appKey, CancellationToken stoppingToken)
    {
        var users = await appFactory.SystemUsers.SystemUsers(appKey);
        var userModels = users.Select(u => u.ToModel()).ToArray();
        return userModels;
    }
}