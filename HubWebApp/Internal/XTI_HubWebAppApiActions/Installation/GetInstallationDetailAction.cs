namespace XTI_HubWebAppApiActions.Installation;

public sealed class GetInstallationDetailAction : AppAction<InstallationIDRequest, InstallationDetailModel>
{
    private readonly CurrentAppUser currentUser;
    private readonly EfHubDB db;
    private readonly AppFromPath appFromPath;

    public GetInstallationDetailAction(CurrentAppUser currentUser, EfHubDB db, AppFromPath appFromPath)
    {
        this.currentUser = currentUser;
        this.db = db;
        this.appFromPath = appFromPath;
    }

    public async Task<InstallationDetailModel> Execute(InstallationIDRequest requestData, CancellationToken stoppingToken)
    {
        var efApp = await appFromPath.Value(stoppingToken);
        var efInstallation = await efApp.Installation(requestData.InstallationID, stoppingToken);
        var efLocation = await efInstallation.Location(stoppingToken);
        var efRequests = await efInstallation.MostRecentRequests(1, stoppingToken);
        var efAppVersion = await efInstallation.AppVersion(stoppingToken);
        var appPermission = await currentUser.GetPermissionsToApp(efAppVersion.App, stoppingToken);
        if (!appPermission.CanView)
        {
            throw new AccessDeniedException($"Access denied to App '{efAppVersion.App.ToModel().AppKey.Format()}'");
        }
        var detail = new InstallationDetailModel
        (
            InstallLocation: efLocation.ToModel(),
            Installation: efInstallation.ToModel(),
            Version: efAppVersion.Version.ToModel(),
            App: efAppVersion.App.ToModel(),
            MostRecentRequest: efRequests.FirstOrDefault()?.ToModel() ?? new AppRequestModel()
        );
        return detail;
    }
}
