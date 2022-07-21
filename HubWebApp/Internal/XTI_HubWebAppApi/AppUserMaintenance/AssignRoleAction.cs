namespace XTI_HubWebAppApi.AppUserMaintenance;

internal sealed class AssignRoleAction : AppAction<UserRoleRequest, int>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly HubFactory hubFactory;
    private readonly CurrentUser currentUser;
    private readonly ICachedUserContext userContext;

    public AssignRoleAction(UserGroupFromPath userGroupFromPath, HubFactory hubFactory, CurrentUser currentUser, ICachedUserContext userContext)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.hubFactory = hubFactory;
        this.currentUser = currentUser;
        this.userContext = userContext;
    }

    public async Task<int> Execute(UserRoleRequest model, CancellationToken stoppingToken)
    {
        var modifier = await hubFactory.Modifiers.Modifier(model.ModifierID);
        var app = await modifier.App();
        var permission = await currentUser.GetPermissionsToApp(app);
        if (!permission.CanView)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        var role = await app.Role(model.RoleID);
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(model.UserID);
        await user.Modifier(modifier).AssignRole(role);
        userContext.ClearCache(user.ToModel().UserName);
        return role.ToModel().ID;
    }
}