namespace XTI_HubWebAppApi.UserInquiry;

public sealed class GetUserAction : AppAction<int, AppUserModel>
{
    private readonly HubFactory factory;

    public GetUserAction(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppUserModel> Execute(int userID, CancellationToken stoppingToken)
    {
        var user = await factory.Users.User(userID);
        return user.ToModel();
    }
}