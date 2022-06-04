namespace XTI_HubAppApi.ModCategoryInquiry;

public sealed class GetResourceGroupsAction : AppAction<int, ResourceGroupModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetResourceGroupsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ResourceGroupModel[]> Execute(int categoryID, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value();
        var currentVersion = await app.CurrentVersion();
        var modCategory = await app.ModCategory(categoryID);
        var resourceGroups = await modCategory.ResourceGroups(currentVersion);
        return resourceGroups.Select(rg => rg.ToModel()).ToArray();
    }
}