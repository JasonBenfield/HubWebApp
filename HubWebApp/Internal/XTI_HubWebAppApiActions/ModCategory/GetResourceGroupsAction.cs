namespace XTI_HubWebAppApiActions.ModCategoryInquiry;

public sealed class GetResourceGroupsAction : AppAction<int, ResourceGroupModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetResourceGroupsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ResourceGroupModel[]> Execute(int categoryID, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value(stoppingToken);
        var currentVersion = await app.CurrentVersion(stoppingToken);
        var modCategory = await app.ModCategory(categoryID, stoppingToken);
        var resourceGroups = await modCategory.ResourceGroups(currentVersion, stoppingToken);
        return resourceGroups.Select(rg => rg.ToModel()).ToArray();
    }
}