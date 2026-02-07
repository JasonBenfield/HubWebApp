namespace XTI_HubWebAppApiActions.Installations;

public sealed class GetInstallationDetailAction : AppAction<InstallationIDRequest, InstallationDetailModel>
{
    private readonly CurrentAppUser currentUser;
    private readonly EfHubDB hubFactory;

    public GetInstallationDetailAction(CurrentAppUser currentUser, EfHubDB hubFactory)
    {
        this.currentUser = currentUser;
        this.hubFactory = hubFactory;
    }

    public async Task<InstallationDetailModel> Execute(InstallationIDRequest requestData, CancellationToken stoppingToken)
    {
        var efInstallation = await hubFactory.Installations.InstallationOrDefault(requestData.InstallationID, stoppingToken);
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
