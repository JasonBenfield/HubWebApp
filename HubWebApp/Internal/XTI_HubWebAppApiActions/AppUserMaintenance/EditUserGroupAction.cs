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
        var sourceGroup = await userGroupFromPath.Value();
        var user = await sourceGroup.User(editRequest.UserID);
        var destinationGroup = await hubFactory.UserGroups.UserGroup(editRequest.UserGroupID);
        var permission = await currentUser.GetPermissionsToUserGroup(destinationGroup);
        if (!permission.CanEdit)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        await user.EditUserGroup(destinationGroup);
        return new EmptyActionResult();
    }
}
