using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.ModCategoryInquiry;

public sealed class GetResourceGroupsAction : AppAction<int, ResourceGroupModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetResourceGroupsAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ResourceGroupModel[]> Execute(int categoryID)
    {
        var app = await appFromPath.Value();
        var modCategory = await app.ModCategory(categoryID);
        var resourceGroups = await modCategory.ResourceGroups();
        return resourceGroups.Select(rg => rg.ToModel()).ToArray();
    }
}