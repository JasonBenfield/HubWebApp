namespace XTI_HubWebAppApiActions.UserInquiry;

public sealed class GetUserAction : AppAction<AppUserIDRequest, AppUserModel>
{
    private readonly UserGroupFromPath userGroupFromPath;

    public GetUserAction(UserGroupFromPath userGroupFromPath)
    {
        this.userGroupFromPath = userGroupFromPath;
    }

    public async Task<AppUserModel> Execute(AppUserIDRequest getRequest, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value(stoppingToken);
        var user = await userGroup.User(getRequest.UserID, stoppingToken);
        return user.ToModel();
    }
}