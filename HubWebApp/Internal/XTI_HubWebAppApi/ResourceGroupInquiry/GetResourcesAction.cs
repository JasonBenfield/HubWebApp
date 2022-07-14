namespace XTI_HubWebAppApi.ResourceGroupInquiry;

public sealed class GetResourcesAction : AppAction<GetResourcesRequest, ResourceModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetResourcesAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ResourceModel[]> Execute(GetResourcesRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey);
        var resourceGroup = await version.ResourceGroup(model.GroupID);
        var resources = await resourceGroup.Resources();
        return resources.Select(r => r.ToModel()).ToArray();
    }
}