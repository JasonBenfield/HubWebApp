namespace XTI_HubWebAppApi.UserInquiry;

internal sealed class GetUserAuthenticatorsAction : AppAction<int, UserAuthenticatorModel[]>
{
    private readonly UserGroupFromPath userGroupFromPath;

    public GetUserAuthenticatorsAction(UserGroupFromPath userGroupFromPath)
    {
        this.userGroupFromPath = userGroupFromPath;
    }

    public async Task<UserAuthenticatorModel[]> Execute(int userID, CancellationToken stoppingToken)
    {
        var userGroup = await userGroupFromPath.Value();
        var user = await userGroup.User(userID);
        var authenticators = await user.Authenticators();
        return authenticators;
    }
}
