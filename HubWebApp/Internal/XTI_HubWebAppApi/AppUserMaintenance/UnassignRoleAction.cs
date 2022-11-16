namespace XTI_HubWebAppApi.AppUserMaintenance;

internal sealed class UnassignRoleAction : AppAction<UserRoleRequest, EmptyActionResult>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly HubFactory factory;
    private readonly CurrentAppUser currentUser;
    private readonly IUserCacheManagement userCacheManagement;

    public UnassignRoleAction(UserGroupFromPath userGroupFromPath, HubFactory factory, CurrentAppUser currentUser, IUserCacheManagement userCacheManagement)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.factory = factory;
        this.currentUser = currentUser;
        this.userCacheManagement = userCacheManagement;
    }

    public async Task<EmptyActionResult> Execute(UserRoleRequest model, CancellationToken stoppingToken)
    {
        var modifier = await factory.Modifiers.Modifier(model.ModifierID);
        var app = await modifier.App();
        var permission = await currentUser.GetPermissionsToApp(app);
        if (!permission.CanView)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(model.UserID);
        var role = await app.Role(model.RoleID);
        await user.Modifier(modifier).UnassignRole(role);
        await userCacheManagement.ClearCache(user.ToModel().UserName, stoppingToken);
        return new EmptyActionResult();
    }
}