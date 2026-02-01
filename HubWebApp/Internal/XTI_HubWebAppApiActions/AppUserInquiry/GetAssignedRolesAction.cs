namespace XTI_HubWebAppApiActions.AppUserInquiry;

public sealed class GetAssignedRolesAction : AppAction<UserModifierKey, AppRoleModel[]>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly EfHubDB factory;
    private readonly CurrentAppUser currentUser;

    public GetAssignedRolesAction(UserGroupFromPath userGroupFromPath, EfHubDB factory, CurrentAppUser currentUser)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.factory = factory;
        this.currentUser = currentUser;
    }

    public async Task<AppRoleModel[]> Execute(UserModifierKey model, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value(stoppingToken);
        var user = await userGroup.User(model.UserID, stoppingToken);
        var modifier = await factory.Modifiers.Modifier(model.ModifierID, stoppingToken);
        var app = await modifier.App(stoppingToken);
        var permission = await currentUser.GetPermissionsToApp(app, stoppingToken);
        if (!permission.CanView)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        var assignedRoles = await user.Modifier(modifier).AssignedRoles(stoppingToken);
        return assignedRoles
            .Where(role => !role.IsDenyAccess())
            .Select(role => role.ToModel())
            .ToArray();
    }
}
