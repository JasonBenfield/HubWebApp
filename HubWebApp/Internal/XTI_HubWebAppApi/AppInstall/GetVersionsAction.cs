namespace XTI_HubWebAppApi.AppInstall;

internal sealed class GetVersionsAction : AppAction<GetVersionsRequest, XtiVersionModel[]>
{
    private readonly HubFactory appFactory;

    public GetVersionsAction(HubFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<XtiVersionModel[]> Execute(GetVersionsRequest request, CancellationToken stoppingToken)
    {
        var versions = await appFactory.Versions.VersionsByName(request.ToAppVersionName());
        return versions.Select(v => v.ToModel()).ToArray();
    }
}
