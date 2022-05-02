namespace XTI_HubAppApi.UserInquiry;

public sealed class GetUserByUserNameAction : AppAction<string, AppUserModel>
{
    private readonly HubFactory factory;

    public GetUserByUserNameAction(HubFactory factory)
    {
        this.factory = factory;
    }

    public async Task<AppUserModel> Execute(string userName)
    {
        var user = await factory.Users.UserByUserName(new AppUserName(userName));
        return user.ToModel();
    }
}