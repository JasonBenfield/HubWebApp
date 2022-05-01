namespace XTI_HubAppApi.UserInquiry;

public sealed class GetUserAction : AppAction<int, AppUserModel>
{
    private readonly HubFactory factory;

    public GetUserAction(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppUserModel> Execute(int userID)
    {
        var user = await factory.Users.User(userID);
        return user.ToModel();
    }
}