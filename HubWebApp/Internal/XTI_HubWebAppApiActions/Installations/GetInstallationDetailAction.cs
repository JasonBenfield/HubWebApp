namespace XTI_HubWebAppApiActions.Installations;

public sealed class GetInstallationDetailAction : AppAction<int, InstallationDetailModel>
{
    private readonly CurrentAppUser currentUser;
    private readonly EfHubDB hubFactory;

    public GetInstallationDetailAction(CurrentAppUser currentUser, EfHubDB hubFactory)
    {
        this.currentUser = currentUser;
        this.hubFactory = hubFactory;
    }

    public async Task<InstallationDetailModel> Execute(int installationID, CancellationToken stoppingToken)
    {
        var installation = await hubFactory.Installations.InstallationOrDefault(installationID, stoppingToken);
        var installLocation = await installation.Location(stoppingToken);
        var requests = await installation.MostRecentRequests(1, stoppingToken);
        var appVersion = await installation.AppVersion(stoppingToken);
        var appPermission = await currentUser.GetPermissionsToApp(appVersion.App, stoppingToken);
        if (!appPermission.CanView)
        {
            throw new AccessDeniedException($"Access denied to App '{appVersion.App.ToModel().AppKey.Format()}'");
        }
        var detail = new InstallationDetailModel
        (
            InstallLocation: installLocation.ToModel(),
            Installation: installation.ToModel(),
            Version: appVersion.Version.ToModel(),
            App: appVersion.App.ToModel(),
            MostRecentRequest: requests.FirstOrDefault()?.ToModel() ?? new AppRequestModel()
        );
        return detail;
    }
}
