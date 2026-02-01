namespace XTI_HubWebAppApiActions.AppPublish;

public sealed class GetVersionsAction : AppAction<AppKeyRequest, XtiVersionModel[]>
{
    private readonly EfHubDB appFactory;

    public GetVersionsAction(EfHubDB appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<XtiVersionModel[]> Execute(AppKeyRequest appKey, CancellationToken stoppingToken)
    {
        var app = await appFactory.Apps.App(appKey.ToAppKey(), stoppingToken);
        var versions = await app.Versions(stoppingToken);
        var versionModels = versions.Select(v => v.ToModel()).ToArray();
        return versionModels;
    }
}