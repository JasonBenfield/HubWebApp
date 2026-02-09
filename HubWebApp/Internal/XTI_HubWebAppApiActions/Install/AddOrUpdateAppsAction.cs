namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class AddOrUpdateAppsAction : AppAction<AddOrUpdateAppsRequest, AppModel[]>
{
    private readonly IHubService hubService;

    public AddOrUpdateAppsAction(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public Task<AppModel[]> Execute(AddOrUpdateAppsRequest addRequest, CancellationToken stoppingToken) =>
        hubService.AddOrUpdateApps
        (
            versionName: addRequest.ToAppVersionName(),
            repoOwner: addRequest.RepoOwner,
            repoName: addRequest.RepoName,
            appKeys: addRequest.ToAppKeys(),
            ct: stoppingToken
        );
}
