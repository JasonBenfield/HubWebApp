namespace XTI_HubWebAppApi;

public sealed class CurrentUser
{
    private readonly HubFactory hubFactory;
    private readonly ICurrentUserName currentUserName;

    public CurrentUser(HubFactory hubFactory, ICurrentUserName currentUserName)
    {
        this.hubFactory = hubFactory;
        this.currentUserName = currentUserName;
    }

    public async Task<AppUser> Value()
    {
        var userName = await currentUserName.Value();
        var user = await hubFactory.Users.UserByUserName(userName);
        return user;
    }
}
