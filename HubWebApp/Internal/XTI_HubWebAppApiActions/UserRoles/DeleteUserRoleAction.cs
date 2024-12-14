namespace XTI_HubWebAppApiActions.UserRoles;

public sealed class DeleteUserRoleAction : AppAction<UserRoleIDRequest, EmptyActionResult>
{
    private readonly HubFactory hubFactory;
    private readonly CurrentAppUser currentUser;

    public DeleteUserRoleAction(HubFactory hubFactory, CurrentAppUser currentUser)
    {
        this.hubFactory = hubFactory;
        this.currentUser = currentUser;
    }

    public async Task<EmptyActionResult> Execute(UserRoleIDRequest deleteRequest, CancellationToken stoppingToken)
    {
        var userRole = await hubFactory.UserRoles.UserRole(deleteRequest.UserRoleID);
        var user = await userRole.User();
        var userGroup = await user.UserGroup();
        var userGroupPermission = await currentUser.GetPermissionsToUserGroup(userGroup);
        if (!userGroupPermission.CanEdit)
        {
            throw new AccessDeniedException($"Access denied to user '{userGroup.ToModel().GroupName}'");
        }
        await userRole.Delete();
        return new EmptyActionResult();
    }
}
