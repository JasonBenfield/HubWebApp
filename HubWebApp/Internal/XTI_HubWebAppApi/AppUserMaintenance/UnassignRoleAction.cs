namespace XTI_HubWebAppApi.AppUserMaintenance;

internal sealed class UnassignRoleAction : AppAction<UserRoleRequest, EmptyActionResult>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly HubFactory factory;
    private readonly CurrentAppUser currentUser;
    private readonly ICachedUserContext userContext;

    public UnassignRoleAction(UserGroupFromPath userGroupFromPath, HubFactory factory, CurrentAppUser currentUser, ICachedUserContext userContext)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.factory = factory;
        this.currentUser = currentUser;
        this.userContext = userContext;
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
        userContext.ClearCache(user.ToModel().UserName);
        return new EmptyActionResult();
    }
}