namespace XTI_HubWebAppApiActions;

public sealed class AppFromPath
{
    private readonly EfHubDB db;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public AppFromPath(EfHubDB db, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.db = db;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public async Task<EfApp> Value(CancellationToken ct)
    {
        var modKey = modifierKeyAccessor.Value();
        if (modKey.Equals(ModifierKey.Default))
        {
            throw new Exception(AppErrors.ModifierIsRequired);
        }
        var efHubApp = await db.Apps.App(HubInfo.AppKey, ct);
        var efModCategory = await efHubApp.ModCategory(HubInfo.ModCategories.Apps, ct);
        var efModifier = await efModCategory.ModifierByModKey(modKey, ct);
        var efApp = await db.Apps.App(efModifier.TargetID(), ct);
        return efApp;
    }
}