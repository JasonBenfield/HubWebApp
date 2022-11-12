namespace XTI_HubWebAppApi.AppUserMaintenance;

internal sealed class DenyAccessAction : AppAction<UserModifierKey, EmptyActionResult>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly HubFactory hubFactory;
    private readonly CurrentAppUser currentUser;
    private readonly ICachedUserContext userContext;

    public DenyAccessAction(UserGroupFromPath userGroupFromPath, HubFactory hubFactory, CurrentAppUser currentUser, ICachedUserContext userContext)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.hubFactory = hubFactory;
        this.currentUser = currentUser;
        this.userContext = userContext;
    }

    public async Task<EmptyActionResult> Execute(UserModifierKey model, CancellationToken stoppingToken)
    {
        var modifier = await hubFactory.Modifiers.Modifier(model.ModifierID);
        var app = await modifier.App();
        var permission = await currentUser.GetPermissionsToApp(app);
        if (!permission.CanView)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        var denyAccessRole = await app.Role(AppRoleName.DenyAccess);
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(model.UserID);
        var existingRoles = await user.Modifier(modifier).ExplicitlyAssignedRoles();
        foreach(var role in existingRoles)
        {
            await user.Modifier(modifier).UnassignRole(role);
        }
        await user.Modifier(modifier).AssignRole(denyAccessRole);
        userContext.ClearCache(user.ToModel().UserName);
        return new EmptyActionResult();
    }
}