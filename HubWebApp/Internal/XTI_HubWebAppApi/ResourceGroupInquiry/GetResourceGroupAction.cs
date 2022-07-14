namespace XTI_HubWebAppApi.ResourceGroupInquiry;

public sealed class GetResourceGroupAction : AppAction<GetResourceGroupRequest, ResourceGroupModel>
{
    private readonly AppFromPath appFromPath;

    public GetResourceGroupAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ResourceGroupModel> Execute(GetResourceGroupRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey);
        var group = await version.ResourceGroup(model.GroupID);
        return group.ToModel();
    }
}