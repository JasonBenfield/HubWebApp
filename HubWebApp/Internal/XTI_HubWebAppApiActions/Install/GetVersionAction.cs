namespace XTI_HubWebAppApiActions.AppInstall;

public sealed class GetVersionAction : AppAction<GetVersionRequest, XtiVersionModel>
{
    private readonly EfHubDB appFactory;

    public GetVersionAction(EfHubDB appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<XtiVersionModel> Execute(GetVersionRequest model, CancellationToken stoppingToken)
    {
        var version = await appFactory.Versions.VersionByName
        (
            model.ToAppVersionName(), 
            model.ToAppVersionKey(),
            stoppingToken
        );
        return version.ToModel();
    }
}