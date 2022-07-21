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

    public async Task<AppUserGroupPermission> GetPermissionsToUser(AppUser user)
    {
        var currentUser = await Value();
        var userGroup = await user.UserGroup();
        var userGroupPermission = await currentUser.GetUserGroupPermission(userGroup);
        return userGroupPermission;
    }

    public async Task<AppPermission> GetPermissionsToApp(App app)
    {
        var currentUser = await Value();
        var appPermission = await currentUser.GetAppPermission(app);
        return appPermission;
    }
}
