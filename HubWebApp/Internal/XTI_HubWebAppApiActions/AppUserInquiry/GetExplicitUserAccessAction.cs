namespace XTI_HubWebAppApiActions.AppUserInquiry;

public sealed class GetExplicitUserAccessAction : AppAction<UserModifierKey, UserAccessModel>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly EfHubDB factory;
    private readonly CurrentAppUser currentUser;

    public GetExplicitUserAccessAction(UserGroupFromPath userGroupFromPath, EfHubDB factory, CurrentAppUser currentUser)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.factory = factory;
        this.currentUser = currentUser;
    }

    public async Task<UserAccessModel> Execute(UserModifierKey model, CancellationToken stoppingToken)
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
        var roles = await user.Modifier(modifier).ExplicitlyAssignedRoles(stoppingToken);
        var roleModels = roles
            .Where(r => !r.IsDenyAccess())
            .Select(r => r.ToModel())
            .ToArray();
        var hasAccess = !roles.Any(r => r.IsDenyAccess());
        return new UserAccessModel(hasAccess, roleModels);
    }
}