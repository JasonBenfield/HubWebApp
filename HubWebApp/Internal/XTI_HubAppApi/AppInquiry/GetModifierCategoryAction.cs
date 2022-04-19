using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppInquiry;

public sealed class GetModifierCategoryAction : AppAction<string, ModifierCategoryModel>
{
    private readonly AppFromPath appFromPath;

    public GetModifierCategoryAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ModifierCategoryModel> Execute(string model)
    {
        var app = await appFromPath.Value();
        var modCategory = await app.ModCategory(new ModifierCategoryName(model));
        var modCategoryModel = modCategory.ToModel();
        return modCategoryModel;
    }
}