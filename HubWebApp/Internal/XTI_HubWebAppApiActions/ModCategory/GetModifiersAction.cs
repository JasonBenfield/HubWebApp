namespace XTI_HubWebAppApiActions.ModCategoryInquiry;

public sealed class GetModifiersAction : AppAction<int, ModifierModel[]>
{
    private readonly AppFromPath appFromPath;

    public GetModifiersAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<ModifierModel[]> Execute(int modCategoryID, CancellationToken stoppingToken)
    {
        var app = await appFromPath.Value(stoppingToken);
        var modCategory = await app.ModCategory(modCategoryID, stoppingToken);
        var modifiers = await modCategory.Modifiers(stoppingToken);
        return modifiers.Select(m => m.ToModel()).ToArray();
    }
}