namespace XTI_HubWebAppApiActions.UserInquiry;

public sealed class GetUserAuthenticatorsAction : AppAction<AppUserIDRequest, UserAuthenticatorModel[]>
{
    private readonly UserGroupFromPath userGroupFromPath;

    public GetUserAuthenticatorsAction(UserGroupFromPath userGroupFromPath)
    {
        this.userGroupFromPath = userGroupFromPath;
    }

    public async Task<UserAuthenticatorModel[]> Execute(AppUserIDRequest getRequest, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value(stoppingToken);
        var user = await userGroup.User(getRequest.UserID, stoppingToken);
        var authenticators = await user.Authenticators(stoppingToken);
        return authenticators;
    }
}
