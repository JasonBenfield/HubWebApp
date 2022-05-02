namespace XTI_HubAppApi.VersionInquiry;

public sealed class GetResourceGroupAction : AppAction<GetVersionResourceGroupRequest, ResourceGroupModel>
{
    private readonly AppFromPath appFromPath;

    public GetResourceGroupAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ResourceGroupModel> Execute(GetVersionResourceGroupRequest model)
    {
        var app = await appFromPath.Value();
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey);
        var groupName = new ResourceGroupName(model.GroupName);
        var group = await version.ResourceGroupByName(groupName);
        var groupModel = group.ToModel();
        return groupModel;
    }
}