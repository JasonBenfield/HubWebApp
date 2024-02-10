namespace XTI_HubWebAppApi.UserGroups;

internal sealed class GetUserGroupForUserAction : AppAction<AppUserIDRequest, AppUserGroupModel>
{
    private readonly HubFactory hubFactory;
    private readonly CurrentAppUser currentUser;

    public GetUserGroupForUserAction(HubFactory hubFactory, CurrentAppUser currentUser)
    {
        this.hubFactory = hubFactory;
        this.currentUser = currentUser;
    }

    public async Task<AppUserGroupModel> Execute(AppUserIDRequest getRequest, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.User(getRequest.UserID);
        var userGroup = await user.UserGroup();
        var userGroupPermissions = await currentUser.GetPermissionsToUserGroup(userGroup);
        if (!userGroupPermissions.CanView)
        {
            throw new AccessDeniedException("Access is denied to this user");
        }
        return userGroup.ToModel();
    }
}
