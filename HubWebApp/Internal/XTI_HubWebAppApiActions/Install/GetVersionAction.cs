namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class GetVersionAction : AppAction<GetVersionRequest, XtiVersionModel>
{
    private readonly HubFactory appFactory;

    public GetVersionAction(HubFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<XtiVersionModel> Execute(GetVersionRequest model, CancellationToken stoppingToken)
    {
        var version = await appFactory.Versions.VersionByName
        (
            model.ToAppVersionName(), 
            model.ToAppVersionKey()
        );
        return version.ToModel();
    }
}