namespace XTI_HubWebAppApi.AppUserInquiry;

public sealed class GetUserAccessByUserModifierAction : AppAction<UserModifierKey, UserAccessModel>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly HubFactory factory;
    private readonly CurrentUser currentUser;

    public GetUserAccessByUserModifierAction(UserGroupFromPath userGroupFromPath, HubFactory factory, CurrentUser currentUser)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.factory = factory;
        this.currentUser = currentUser;
    }

    public async Task<UserAccessModel> Execute(UserModifierKey model, CancellationToken stoppingToken)
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
        var roles = await user.Modifier(modifier).ExplicitlyAssignedRoles();
        var roleModels = roles
            .Where(r => !r.IsDenyAccess())
            .Select(r => r.ToModel())
            .ToArray();
        var hasAccess = !roles.Any(r => r.IsDenyAccess());
        return new UserAccessModel(hasAccess, roleModels);
    }
}