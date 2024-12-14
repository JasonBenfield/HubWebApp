namespace XTI_HubWebAppApiActions.AppUserInquiry;

public sealed class GetExplicitlyUnassignedRolesAction : AppAction<UserModifierKey, AppRoleModel[]>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly HubFactory factory;
    private readonly CurrentAppUser currentUser;

    public GetExplicitlyUnassignedRolesAction(UserGroupFromPath userGroupFromPath, HubFactory factory, CurrentAppUser currentUser)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.factory = factory;
        this.currentUser = currentUser;
    }

    public async Task<AppRoleModel[]> Execute(UserModifierKey model, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(model.UserID);
        var modifier = await factory.Modifiers.Modifier(model.ModifierID);
        var app = await modifier.App();
        var permission = await currentUser.GetPermissionsToApp(app);
        if (!permission.CanView)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        var unassignedRoles = await user.Modifier(modifier).ExplicitlyUnassignedRoles();
        return unassignedRoles
            .Where(role => !role.IsDenyAccess())
            .Select(role => role.ToModel())
            .ToArray();
    }
}