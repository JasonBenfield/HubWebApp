namespace XTI_HubWebAppApi.UserInquiry;

internal sealed class GetUserAuthenticatorsAction : AppAction<AppUserIDRequest, UserAuthenticatorModel[]>
{
    private readonly UserGroupFromPath userGroupFromPath;

    public GetUserAuthenticatorsAction(UserGroupFromPath userGroupFromPath)
    {
        this.userGroupFromPath = userGroupFromPath;
    }

    public async Task<UserAuthenticatorModel[]> Execute(AppUserIDRequest getRequest, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(getRequest.UserID);
        var authenticators = await user.Authenticators();
        return authenticators;
    }
}
