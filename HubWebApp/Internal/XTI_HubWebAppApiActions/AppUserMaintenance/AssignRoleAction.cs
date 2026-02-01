namespace XTI_HubWebAppApiActions.AppUserMaintenance;

public sealed class AssignRoleAction : AppAction<UserRoleRequest, int>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly EfHubDB hubFactory;
    private readonly CurrentAppUser currentUser;
    private readonly IUserCacheManagement userCacheManagement;

    public AssignRoleAction(UserGroupFromPath userGroupFromPath, EfHubDB hubFactory, CurrentAppUser currentUser, IUserCacheManagement userCacheManagement)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.hubFactory = hubFactory;
        this.currentUser = currentUser;
        this.userCacheManagement = userCacheManagement;
    }

    public async Task<int> Execute(UserRoleRequest assignRequest, CancellationToken stoppingToken)
    {
        var modifier = await hubFactory.Modifiers.Modifier(assignRequest.ModifierID, stoppingToken);
        var app = await modifier.App(stoppingToken);
        var permission = await currentUser.GetPermissionsToApp(app, stoppingToken);
        if (!permission.CanView)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        var role = await app.Role(assignRequest.RoleID, stoppingToken);
        var userGroup = await userGroupFromPath.Value(stoppingToken);
        var user = await userGroup.User(assignRequest.UserID, stoppingToken);
        await user.Modifier(modifier).AssignRole(role, stoppingToken);
        await userCacheManagement.ClearCache(user.ToModel().UserName, stoppingToken);
        return role.ToModel().ID;
    }
}