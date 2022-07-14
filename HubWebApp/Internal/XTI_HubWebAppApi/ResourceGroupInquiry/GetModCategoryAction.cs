namespace XTI_HubWebAppApi.ResourceGroupInquiry;

public sealed class GetModCategoryAction : AppAction<GetResourceGroupModCategoryRequest, ModifierCategoryModel>
{
    private readonly AppFromPath appFromPath;

    public GetModCategoryAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ModifierCategoryModel> Execute(GetResourceGroupModCategoryRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var versionKey = AppVersionKey.Parse(model.VersionKey);
        var version = await app.Version(versionKey);
        var group = await version.ResourceGroup(model.GroupID);
        var modCategory = await group.ModCategory();
        return modCategory.ToModel();
    }
}