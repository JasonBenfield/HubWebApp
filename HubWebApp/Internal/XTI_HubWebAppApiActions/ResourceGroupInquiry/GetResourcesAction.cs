namespace XTI_HubWebAppApiActions.ResourceGroupInquiry;

public sealed class GetResourcesAction : AppAction<GetResourcesRequest, ResourceModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetResourcesAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ResourceModel[]> Execute(GetResourcesRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value(stoppingToken);
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey, stoppingToken);
        var resourceGroup = await version.ResourceGroup(model.GroupID, stoppingToken);
        var resources = await resourceGroup.Resources(stoppingToken);
        return resources.Select(r => r.ToModel()).ToArray();
    }
}