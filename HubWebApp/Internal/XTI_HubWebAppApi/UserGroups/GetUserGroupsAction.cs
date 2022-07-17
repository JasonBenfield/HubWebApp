namespace XTI_HubWebAppApi.UserGroups;

internal sealed class GetUserGroupsAction : AppAction<EmptyRequest, AppUserGroupModel[]>
{
    private readonly IUserContext userContext;
    private readonly HubFactory hubFactory;

    public GetUserGroupsAction(IUserContext userContext, HubFactory hubFactory)
    {
        this.userContext = userContext;
        this.hubFactory = hubFactory;
    }

    public async Task<AppUserGroupModel[]> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var userContextModel = await userContext.User();
        var user = await hubFactory.Users.User(userContextModel.User.ID);
        var permissions = await user.GetUserGroupPermissions();
        var userGroupModels = permissions.Where(p => p.CanView)
            .Select(p => p.UserGroup.ToModel())
            .ToArray();
        return userGroupModels;
    }
}
