namespace XTI_HubWebAppApiActions.ResourceInquiry;

public sealed class GetResourceAction : AppAction<GetResourceRequest, ResourceModel>
{
    private readonly AppFromPath appFromPath;

    public GetResourceAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ResourceModel> Execute(GetResourceRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value(stoppingToken);
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey, stoppingToken);
        var resource = await version.Resource(model.ResourceID, stoppingToken);
        return resource.ToModel();
    }
}