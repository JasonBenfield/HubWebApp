namespace XTI_HubWebAppApiActions.Logs;

public sealed class GetSessionDetailAction : AppAction<int, AppSessionDetailModel>
{
    private readonly CurrentAppUser currentUser;
    private readonly HubFactory hubFactory;

    public GetSessionDetailAction(CurrentAppUser currentUser, HubFactory hubFactory)
    {
        this.currentUser = currentUser;
        this.hubFactory = hubFactory;
    }

    public async Task<AppSessionDetailModel> Execute(int sessionID, CancellationToken stoppingToken)
    {
        var session = await hubFactory.Sessions.Session(sessionID, stoppingToken);
        var user = await session.User(stoppingToken);
        var userGroup = await user.UserGroup(stoppingToken);
        var userGroupPermission = await currentUser.GetPermissionsToUserGroup(userGroup, stoppingToken);
        if (!userGroupPermission.CanView)
        {
            throw new AccessDeniedException($"Access denied to user '{userGroup.ToModel().GroupName}'");
        }
        var detail = new AppSessionDetailModel
        (
            Session: session.ToModel(),
            UserGroup: userGroup.ToModel(),
            User: user.ToModel()
        );
        return detail;
    }
}
