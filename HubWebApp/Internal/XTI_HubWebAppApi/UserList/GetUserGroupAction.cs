namespace XTI_HubWebAppApi.UserList;

internal sealed class GetUserGroupAction : AppAction<EmptyRequest, AppUserGroupModel>
{
    private readonly UserGroupFromPath userGroupFromPath;

    public GetUserGroupAction(UserGroupFromPath userGroupFromPath)
    {
        this.userGroupFromPath = userGroupFromPath;
    }

    public async Task<AppUserGroupModel> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value();
        return userGroup.ToModel();
    }
}
