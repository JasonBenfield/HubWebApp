namespace XTI_HubWebAppApi.UserGroups;

internal sealed class GetUserDetailOrAnonAction : AppAction<AppUserNameRequest, AppUserDetailModel>
{
    private readonly HubFactory hubFactory;
    private readonly CurrentAppUser currentUser;

    public GetUserDetailOrAnonAction(HubFactory hubFactory, CurrentAppUser currentUser)
    {
        this.hubFactory = hubFactory;
        this.currentUser = currentUser;
    }

    public async Task<AppUserDetailModel> Execute(AppUserNameRequest getRequest, CancellationToken stoppingToken)
    {
        var user = await hubFactory.Users.UserOrAnon(getRequest.ToAppUserName());
        var userGroup = await user.UserGroup();
        var userGroupPermissions = await currentUser.GetPermissionsToUserGroup(userGroup);
        if (!userGroupPermissions.CanView)
        {
            throw new AccessDeniedException("Access denied to this user");
        }
        return new AppUserDetailModel(user.ToModel(), userGroup.ToModel());
    }
}
