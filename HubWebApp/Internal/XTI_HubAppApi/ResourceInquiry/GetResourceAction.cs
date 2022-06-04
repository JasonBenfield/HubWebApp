namespace XTI_HubAppApi.ResourceInquiry;

public sealed class GetResourceAction : AppAction<GetResourceRequest, ResourceModel>
{
    private readonly AppFromPath appFromPath;

    public GetResourceAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ResourceModel> Execute(GetResourceRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey);
        var resource = await version.Resource(model.ResourceID);
        return resource.ToModel();
    }
}