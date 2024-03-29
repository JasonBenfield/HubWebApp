﻿namespace XTI_HubWebAppApi;

public sealed class AppFromPath
{
    private readonly HubFactory factory;
    private readonly IXtiPathAccessor pathAccessor;

    public AppFromPath(HubFactory factory, IXtiPathAccessor pathAccessor)
    {
        this.factory = factory;
        this.pathAccessor = pathAccessor;
    }

    public async Task<App> Value()
    {
        var path = pathAccessor.Value();
        var modKey = path.Modifier;
        if (modKey.Equals(ModifierKey.Default))
        {
            throw new Exception(AppErrors.ModifierIsRequired);
        }
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var modCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var modifier = await modCategory.ModifierByModKey(modKey);
        var app = await factory.Apps.App(modifier.TargetID());
        return app;
    }
}