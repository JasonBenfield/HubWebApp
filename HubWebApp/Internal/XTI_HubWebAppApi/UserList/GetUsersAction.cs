namespace XTI_HubWebAppApi.UserList;

public sealed class GetUsersAction : AppAction<EmptyRequest, AppUserModel[]>
{
    private readonly HubFactory factory;

    public GetUsersAction(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppUserModel[]> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var users = await factory.Users.Users();
        return users.Select(u => u.ToModel()).ToArray();
    }
}