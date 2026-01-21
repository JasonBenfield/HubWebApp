namespace XTI_HubWebAppApiActions.ResourceGroupInquiry;

public sealed class GetResourceGroupAction : AppAction<GetResourceGroupRequest, ResourceGroupModel>
{
    private readonly AppFromPath appFromPath;

    public GetResourceGroupAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ResourceGroupModel> Execute(GetResourceGroupRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value(stoppingToken);
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey, stoppingToken);
        var group = await version.ResourceGroup(model.GroupID, stoppingToken);
        return group.ToModel();
    }
}