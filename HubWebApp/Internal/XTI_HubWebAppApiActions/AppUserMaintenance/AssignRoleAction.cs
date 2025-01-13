namespace XTI_HubWebAppApiActions.AppUserMaintenance;

public sealed class AssignRoleAction : AppAction<UserRoleRequest, int>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly HubFactory hubFactory;
    private readonly CurrentAppUser currentUser;
    private readonly IUserCacheManagement userCacheManagement;

    public AssignRoleAction(UserGroupFromPath userGroupFromPath, HubFactory hubFactory, CurrentAppUser currentUser, IUserCacheManagement userCacheManagement)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.hubFactory = hubFactory;
        this.currentUser = currentUser;
        this.userCacheManagement = userCacheManagement;
    }

    public async Task<int> Execute(UserRoleRequest assignRequest, CancellationToken stoppingToken)
    {
        var modifier = await hubFactory.Modifiers.Modifier(assignRequest.ModifierID);
        var app = await modifier.App();
        var permission = await currentUser.GetPermissionsToApp(app);
        if (!permission.CanView)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        var role = await app.Role(assignRequest.RoleID);
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(assignRequest.UserID);
        await user.Modifier(modifier).AssignRole(role);
        await userCacheManagement.ClearCache(user.ToModel().UserName, stoppingToken);
        return role.ToModel().ID;
    }
}