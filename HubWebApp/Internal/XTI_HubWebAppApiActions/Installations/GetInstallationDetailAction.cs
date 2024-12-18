namespace XTI_HubWebAppApiActions.Installations;

public sealed class GetInstallationDetailAction : AppAction<int, InstallationDetailModel>
{
    private readonly CurrentAppUser currentUser;
    private readonly HubFactory hubFactory;

    public GetInstallationDetailAction(CurrentAppUser currentUser, HubFactory hubFactory)
    {
        this.currentUser = currentUser;
        this.hubFactory = hubFactory;
    }

    public async Task<InstallationDetailModel> Execute(int installationID, CancellationToken stoppingToken)
    {
        var installation = await hubFactory.Installations.InstallationOrDefault(installationID);
        var installLocation = await installation.Location();
        var requests = await installation.MostRecentRequests(1);
        var appVersion = await installation.AppVersion();
        var appPermission = await currentUser.GetPermissionsToApp(appVersion.App);
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
