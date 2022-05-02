namespace XTI_HubAppApi.AppPublish;

public sealed class GetVersionsAction : AppAction<AppKey, XtiVersionModel[]>
{
    private readonly HubFactory appFactory;

    public GetVersionsAction(HubFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<XtiVersionModel[]> Execute(AppKey appKey)
    {
        var app = await appFactory.Apps.App(appKey);
        var versions = await app.Versions();
        var versionModels = versions.Select(v => v.ToModel()).ToArray();
        return versionModels;
    }
}