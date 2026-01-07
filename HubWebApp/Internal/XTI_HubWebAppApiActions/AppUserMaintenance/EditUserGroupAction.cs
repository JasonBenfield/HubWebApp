namespace XTI_HubWebAppApiActions.AppUserMaintenance;

public sealed class EditUserGroupAction : AppAction<EditUserGroupRequest, EmptyActionResult>
{
    private readonly UserGroupFromPath userGroupFromPath;
    private readonly HubFactory hubFactory;
    private readonly CurrentAppUser currentUser;

    public EditUserGroupAction(UserGroupFromPath userGroupFromPath, HubFactory hubFactory, CurrentAppUser currentUser)
    {
        this.userGroupFromPath = userGroupFromPath;
        this.hubFactory = hubFactory;
        this.currentUser = currentUser;
    }

    public async Task<EmptyActionResult> Execute(EditUserGroupRequest editRequest, CancellationToken stoppingToken)
    {
        var sourceGroup = await userGroupFromPath.Value(stoppingToken);
        var user = await sourceGroup.User(editRequest.UserID, stoppingToken);
        var destinationGroup = await hubFactory.UserGroups.UserGroup(editRequest.UserGroupID, stoppingToken);
        var permission = await currentUser.GetPermissionsToUserGroup(destinationGroup, stoppingToken);
        if (!permission.CanEdit)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        await user.EditUserGroup(destinationGroup, stoppingToken);
        return new EmptyActionResult();
    }
}
