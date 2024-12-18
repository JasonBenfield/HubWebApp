namespace XTI_HubWebAppApiActions.UserInquiry;

public class GetUserOrAnonAction : AppAction<AppUserNameRequest, AppUserModel>
{
    private readonly UserGroupFromPath userGroupFromPath;

    public GetUserOrAnonAction(UserGroupFromPath userGroupFromPath)
    {
        this.userGroupFromPath = userGroupFromPath;
    }

    public async Task<AppUserModel> Execute(AppUserNameRequest getRequest, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.UserOrAnon(getRequest.ToAppUserName());
        return user.ToModel();
    }
}
