namespace XTI_HubWebAppApi.UserGroups;

internal sealed class GetUserGroupsAction : AppAction<EmptyRequest, AppUserGroupModel[]>
{
    private readonly CurrentAppUser currentUser;

    public GetUserGroupsAction(CurrentAppUser currentUser)
    {
        this.currentUser = currentUser;
    }

    public async Task<AppUserGroupModel[]> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var user = await currentUser.Value();
        var permissions = await user.GetUserGroupPermissions();
        var userGroupModels = permissions.Where(p => p.CanView)
            .Select(p => p.UserGroup.ToModel())
            .OrderBy(ug => ug.GroupName.DisplayText)
            .ToArray();
        return userGroupModels;
    }
}
