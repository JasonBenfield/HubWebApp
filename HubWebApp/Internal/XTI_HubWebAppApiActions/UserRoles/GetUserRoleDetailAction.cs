namespace XTI_HubWebAppApiActions.UserRoles;

public sealed class GetUserRoleDetailAction : AppAction<UserRoleIDRequest, UserRoleDetailModel>
{
    private readonly CurrentAppUser currentUser;
    private readonly HubFactory hubFactory;

    public GetUserRoleDetailAction(CurrentAppUser currentUser, HubFactory hubFactory)
    {
        this.currentUser = currentUser;
        this.hubFactory = hubFactory;
    }

    public async Task<UserRoleDetailModel> Execute(UserRoleIDRequest getRequest, CancellationToken stoppingToken)
    {
        var userRole = await hubFactory.UserRoles.UserRole(getRequest.UserRoleID, stoppingToken);
        var user = await userRole.User(stoppingToken);
        var userGroup = await user.UserGroup(stoppingToken);
        var userGroupPermission = await currentUser.GetPermissionsToUserGroup(userGroup, stoppingToken);
        if (!userGroupPermission.CanView)
        {
            throw new AccessDeniedException($"Access denied to user '{userGroup.ToModel().GroupName}'");
        }
        var role = await userRole.Role(stoppingToken);
        var app = await role.App(stoppingToken);
        var appPermission = await currentUser.GetPermissionsToApp(app, stoppingToken);
        if (!appPermission.CanView)
        {
            throw new AccessDeniedException($"Access denied to app '{app.ToModel().AppKey.Format()}'");
        }
        var modifier = await userRole.Modifier(stoppingToken);
        var modCategory = await modifier.Category(stoppingToken);
        return new UserRoleDetailModel
        (
            userRole.ID, 
            userGroup.ToModel(),
            user.ToModel(), 
            app.ToModel(), 
            role.ToModel(),
            modCategory.ToModel(),
            modifier.ToModel()
        );
    }
}
