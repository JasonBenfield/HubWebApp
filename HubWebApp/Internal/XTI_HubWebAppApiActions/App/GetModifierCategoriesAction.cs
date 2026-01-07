namespace XTI_HubWebAppApiActions.AppInquiry;

public sealed class GetModifierCategoriesAction : AppAction<EmptyRequest, ModifierCategoryModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetModifierCategoriesAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ModifierCategoryModel[]> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value(stoppingToken);
        var modCategories = await app.ModCategories(stoppingToken);
        return modCategories.Select(modCat => modCat.ToModel()).ToArray();
    }
}