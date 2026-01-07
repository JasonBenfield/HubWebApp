namespace XTI_HubWebAppApiActions;

public sealed class AppFromPath
{
    private readonly HubFactory factory;
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    public AppFromPath(HubFactory factory, IModifierKeyAccessor modifierKeyAccessor)
    {
        this.factory = factory;
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    public async Task<App> Value(CancellationToken ct)
    {
        var modKey = modifierKeyAccessor.Value();
        if (modKey.Equals(ModifierKey.Default))
        {
            throw new Exception(AppErrors.ModifierIsRequired);
        }
        var hubApp = await factory.Apps.App(HubInfo.AppKey, ct);
        var modCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps, ct);
        var modifier = await modCategory.ModifierByModKey(modKey, ct);
        var app = await factory.Apps.App(modifier.TargetID(), ct);
        return app;
    }
}