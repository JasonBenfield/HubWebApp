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

    public async Task<AppUser> Value(CancellationToken ct)
    {
        var userName = await currentUserName.Value();
        var user = await hubFactory.Users.UserByUserName(userName, ct);
        return user;
    }

    public async Task<AppUserGroupPermission[]> GetUserGroupPermissions(CancellationToken ct)
    {
        var currentUser = await Value(ct);
        var userGroupPermissions = await currentUser.GetUserGroupPermissions(ct);
        return userGroupPermissions;
    }

    public async Task<AppUserGroupPermission> GetPermissionsToUser(AppUser user, CancellationToken ct)
    {
        var userGroup = await user.UserGroup(ct);
        var permission = await GetPermissionsToUserGroup(userGroup, ct);
        return permission;
    }

    public async Task<AppUserGroupPermission> GetPermissionsToUserGroup(AppUserGroup userGroup, CancellationToken ct)
    {
        var currentUser = await Value(ct);
        var userGroupPermission = await currentUser.GetUserGroupPermission(userGroup, ct);
        return userGroupPermission;
    }

    public async Task<AppPermission[]> GetAppPermissions(CancellationToken ct)
    {
        var currentUser = await Value(ct);
        var appPermissions = await currentUser.GetAppPermissions(ct);
        return appPermissions;
    }

    public async Task<AppPermission> GetPermissionsToApp(App app, CancellationToken ct)
    {
        var currentUser = await Value(ct);
        var appPermission = await currentUser.GetAppPermission(app, ct);
        return appPermission;
    }
}
