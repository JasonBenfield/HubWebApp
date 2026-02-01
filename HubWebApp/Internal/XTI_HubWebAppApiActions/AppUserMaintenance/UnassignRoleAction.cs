namespace XTI_HubWebAppApiActions.AppUserMaintenance;

public sealed class UnassignRoleAction : AppAction<UserRoleRequest, EmptyActionResult>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly EfHubDB factory;
    private readonly CurrentAppUser currentUser;
    private readonly IUserCacheManagement userCacheManagement;

    public UnassignRoleAction(UserGroupFromPath userGroupFromPath, EfHubDB factory, CurrentAppUser currentUser, IUserCacheManagement userCacheManagement)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.factory = factory;
        this.currentUser = currentUser;
        this.userCacheManagement = userCacheManagement;
    }

    public async Task<EmptyActionResult> Execute(UserRoleRequest unassignRequest, CancellationToken stoppingToken)
    {
        var modifier = await factory.Modifiers.Modifier(unassignRequest.ModifierID, stoppingToken);
        var app = await modifier.App(stoppingToken);
        var permission = await currentUser.GetPermissionsToApp(app, stoppingToken);
        if (!permission.CanView)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        var userGroup = await userGroupFromPath.Value(stoppingToken);
        var user = await userGroup.User(unassignRequest.UserID, stoppingToken);
        var role = await app.Role(unassignRequest.RoleID, stoppingToken);
        await user.Modifier(modifier).UnassignRole(role, stoppingToken);
        await userCacheManagement.ClearCache(user.ToModel().UserName, stoppingToken);
        return new EmptyActionResult();
    }
}