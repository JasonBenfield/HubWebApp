namespace XTI_HubWebAppApiActions.AppPublish;

public sealed class GetVersionsAction : AppAction<AppKeyRequest, XtiVersionModel[]>
{
    private readonly HubFactory appFactory;

    public GetVersionsAction(HubFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<XtiVersionModel[]> Execute(AppKeyRequest appKey, CancellationToken stoppingToken)
    {
        var app = await appFactory.Apps.App(appKey.ToAppKey());
        var versions = await app.Versions();
        var versionModels = versions.Select(v => v.ToModel()).ToArray();
        return versionModels;
    }
}