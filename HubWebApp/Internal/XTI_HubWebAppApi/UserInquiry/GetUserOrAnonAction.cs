namespace XTI_HubWebAppApi.UserInquiry;

internal class GetUserOrAnonAction : AppAction<string, AppUserModel>
{
    private readonly UserGroupFromPath userGroupFromPath;

    public GetUserOrAnonAction(UserGroupFromPath userGroupFromPath)
    {
        this.userGroupFromPath = userGroupFromPath;
    }

    public async Task<AppUserModel> Execute(string userName, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.UserOrAnon(new AppUserName(userName));
        return user.ToModel();
    }
}
