namespace XTI_HubWebAppApi.UserInquiry;

public sealed class GetUsersAction : AppAction<EmptyRequest, AppUserModel[]>
{
    private readonly UserGroupFromPath userGroupFromPath;

    public GetUsersAction(UserGroupFromPath userGroupFromPath)
    {
        this.userGroupFromPath = userGroupFromPath;
    }

    public async Task<AppUserModel[]> Execute(EmptyRequest request, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value();
        var users = await userGroup.Users();
        return users.Select(u => u.ToModel()).ToArray();
    }
}