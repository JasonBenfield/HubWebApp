namespace XTI_HubWebAppApi.AppInstall;

internal sealed class AddOrUpdateAppsAction : AppAction<AddOrUpdateAppsRequest, AppModel[]>
{
    private readonly IHubAdministration hubAdmin;

    public AddOrUpdateAppsAction(IHubAdministration hubAdmin)
    {
        this.hubAdmin = hubAdmin;
    }

    public Task<AppModel[]> Execute(AddOrUpdateAppsRequest addRequest, CancellationToken stoppingToken) =>
        hubAdmin.AddOrUpdateApps
        (
            addRequest.ToAppVersionName(),
            addRequest.ToAppKeys(),
            stoppingToken
        );
}
