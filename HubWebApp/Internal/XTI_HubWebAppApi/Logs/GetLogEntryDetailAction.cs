namespace XTI_HubWebAppApi.Logs;

internal sealed class GetLogEntryDetailAction : AppAction<int, AppLogEntryDetailModel>
{
    private readonly CurrentAppUser currentUser;
    private readonly HubFactory hubFactory;

    public GetLogEntryDetailAction(CurrentAppUser currentUser, HubFactory hubFactory)
    {
        this.currentUser = currentUser;
        this.hubFactory = hubFactory;
    }

    public async Task<AppLogEntryDetailModel> Execute(int logEntryID, CancellationToken stoppingToken)
    {
        var logEntry = await hubFactory.LogEntries.LogEntry(logEntryID);
        var request = await logEntry.Request();
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
        var sourceLogEntry = await logEntry.SourceLogEntryOrDefault();
        var targetLogEntry = await logEntry.TargetLogEntryOrDefault();
        var detail = new AppLogEntryDetailModel
        (
            LogEntry: logEntry.ToModel(),
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
            SourceLogEntryID: sourceLogEntry.ToModel().ID,
            TargetLogEntryID: targetLogEntry.ToModel().ID
        );
        return detail;
    }
}
