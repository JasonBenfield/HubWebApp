namespace XTI_HubWebAppApi.UserInquiry;

public sealed class GetUserAction : AppAction<int, AppUserModel>
{
    private readonly UserGroupFromPath userGroupFromPath;

    public GetUserAction(UserGroupFromPath userGroupFromPath)
    {
        this.userGroupFromPath = userGroupFromPath;
    }

    public async Task<AppUserModel> Execute(int userID, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(userID);
        return user.ToModel();
    }
}