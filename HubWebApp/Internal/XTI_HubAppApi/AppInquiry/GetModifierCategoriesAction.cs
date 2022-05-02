namespace XTI_HubAppApi.AppInquiry;

public sealed class GetModifierCategoriesAction : AppAction<EmptyRequest, ModifierCategoryModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetModifierCategoriesAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ModifierCategoryModel[]> Execute(EmptyRequest model)
    {
        var app = await appFromPath.Value();
        var modCategories = await app.ModCategories();
        return modCategories.Select(modCat => modCat.ToModel()).ToArray();
    }
}