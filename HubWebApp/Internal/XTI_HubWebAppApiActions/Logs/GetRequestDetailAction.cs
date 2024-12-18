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
        var request = await hubFactory.Requests.Request(requestID);
        var installation = await request.Installation();
        var installLocation = await installation.Location();
        var resource = await request.Resource();
        var resourceGroup = await resource.Group();
        var modifier = await request.Modifier();
        var modCategory = await modifier.Category();
        var appVersion = await installation.AppVersion();
        var session = await request.Session();
        var user = await session.User();
        var userGroup = await user.UserGroup();
        var userGroupPermission = await currentUser.GetPermissionsToUserGroup(userGroup);
        if (!userGroupPermission.CanView)
        {
            throw new AccessDeniedException($"Access denied to user '{userGroup.ToModel().GroupName}'");
        }
        var appPermission = await currentUser.GetPermissionsToApp(appVersion.App);
        if (!appPermission.CanView)
        {
            throw new AccessDeniedException($"Access denied to App '{appVersion.App.ToModel().AppKey.Format()}'");
        }
        var sourceRequest = await request.SourceRequestOrDefault();
        var targetRequestIDs = await request.TargetRequestIDs();
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
