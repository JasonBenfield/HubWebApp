namespace XTI_HubAppApi.ModCategoryInquiry;

public sealed class GetModCategoryAction : AppAction<int, ModifierCategoryModel>
{
    private readonly AppFromPath appFromPath;

    public GetModCategoryAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ModifierCategoryModel> Execute(int categoryID)
    {
        var app = await appFromPath.Value();
        var modCategory = await app.ModCategory(categoryID);
        return modCategory.ToModel();
    }
}