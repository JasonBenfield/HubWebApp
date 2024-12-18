namespace XTI_HubWebAppApiActions;

public sealed class CurrentAppUser
{
    private readonly HubFactory hubFactory;
    private readonly ICurrentUserName currentUserName;

    public CurrentAppUser(HubFactory hubFactory, ICurrentUserName currentUserName)
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

    public async Task<AppUserGroupPermission[]> GetUserGroupPermissions()
    {
        var currentUser = await Value();
        var userGroupPermissions = await currentUser.GetUserGroupPermissions();
        return userGroupPermissions;
    }

    public async Task<AppUserGroupPermission> GetPermissionsToUser(AppUser user)
    {
        var userGroup = await user.UserGroup();
        var permission = await GetPermissionsToUserGroup(userGroup);
        return permission;
    }

    public async Task<AppUserGroupPermission> GetPermissionsToUserGroup(AppUserGroup userGroup)
    {
        var currentUser = await Value();
        var userGroupPermission = await currentUser.GetUserGroupPermission(userGroup);
        return userGroupPermission;
    }

    public async Task<AppPermission[]> GetAppPermissions()
    {
        var currentUser = await Value();
        var appPermissions = await currentUser.GetAppPermissions();
        return appPermissions;
    }

    public async Task<AppPermission> GetPermissionsToApp(App app)
    {
        var currentUser = await Value();
        var appPermission = await currentUser.GetAppPermission(app);
        return appPermission;
    }
}
