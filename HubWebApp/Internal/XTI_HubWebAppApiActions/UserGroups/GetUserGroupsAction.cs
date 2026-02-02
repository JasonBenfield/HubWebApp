namespace XTI_HubWebAppApiActions.UserGroups;

public sealed class GetUserGroupsAction : AppAction<EmptyRequest, AppUserGroupModel[]>
{
    private readonly CurrentAppUser currentUser;

    public GetUserGroupsAction(CurrentAppUser currentUser)
    {
        this.currentUser = currentUser;
    }

    public async Task<AppUserGroupModel[]> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var user = await currentUser.Value(stoppingToken);
        var permissions = await user.GetUserGroupPermissions(stoppingToken);
        var userGroupModels = permissions.Where(p => p.CanView)
            .Select(p => p.EfUserGroup.ToModel())
            .OrderBy(ug => ug.GroupName.DisplayText)
            .ToArray();
        return userGroupModels;
    }
}
