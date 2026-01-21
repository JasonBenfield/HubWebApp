namespace XTI_HubWebAppApiActions.Logs;

public sealed class GetRequestDetailAction : AppAction<int, AppRequestDetailModel>
{
    private readonly CurrentAppUser currentUser;
    private readonly HubFactory hubFactory;

    public GetRequestDetailAction(CurrentAppUser currentUser, HubFactory hubFactory)
    {
        this.currentUser = currentUser;
        this.hubFactory = hubFactory;
    }

    public async Task<AppRequestDetailModel> Execute(int requestID, CancellationToken stoppingToken)
    {
        var request = await hubFactory.Requests.Request(requestID, stoppingToken);
        var installation = await request.Installation(stoppingToken);
        var installLocation = await installation.Location(stoppingToken);
        var resource = await request.Resource(stoppingToken);
        var resourceGroup = await resource.Group(stoppingToken);
        var modifier = await request.Modifier(stoppingToken);
        var modCategory = await modifier.Category(stoppingToken);
        var appVersion = await installation.AppVersion(stoppingToken);
        var session = await request.Session(stoppingToken);
        var user = await session.User(stoppingToken);
        var userGroup = await user.UserGroup(stoppingToken);
        var userGroupPermission = await currentUser.GetPermissionsToUserGroup(userGroup, stoppingToken);
        if (!userGroupPermission.CanView)
        {
            throw new AccessDeniedException($"Access denied to user '{userGroup.ToModel().GroupName}'");
        }
        var appPermission = await currentUser.GetPermissionsToApp(appVersion.App, stoppingToken);
        if (!appPermission.CanView)
        {
            throw new AccessDeniedException($"Access denied to App '{appVersion.App.ToModel().AppKey.Format()}'");
        }
        var sourceRequest = await request.SourceRequestOrDefault(stoppingToken);
        var targetRequestIDs = await request.TargetRequestIDs(stoppingToken);
        var detail = new AppRequestDetailModel
        (
            Request: request.ToModel(),
            ResourceGroup: resourceGroup.ToModel(),
            Resource: resource.ToModel(),
            ModCategory: modCategory.ToModel(),
            Modifier: modifier.ToModel(),
            InstallLocation: installLocation.ToModel(),
            Installation: installation.ToModel(),
            Version: appVersion.Version.ToModel(),
            App: appVersion.App.ToModel(),
            Session: session.ToModel(),
            UserGroup: userGroup.ToModel(),
            User: user.ToModel(),
            SourceRequestID: sourceRequest.ToModel().ID,
            TargetRequestIDs: targetRequestIDs,
            RequestData: request.RequestData,
            ResultData: request.ResultData
        );
        return detail;
    }
}
