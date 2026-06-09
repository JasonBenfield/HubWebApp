using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

namespace HubWebApp.Tests;

internal static class TestExtensions
{
    public static Task<ModifierModel> HubAppModifier(this IServiceProvider sp) => sp.AppModifier(HubInfo.AppKey);

    public static async Task<ModifierModel> AppModifier(this IServiceProvider sp, AppKey appKey)
    {
        var factory = sp.GetRequiredService<EfHubDB>();
        var app = await factory.Apps.App(appKey, ct: default);
        var appModel = app.ToModel();
        EfApp hubApp;
        if (appKey.Equals(HubInfo.AppKey))
        {
            hubApp = app;
        }
        else
        {
            hubApp = await sp.HubApp();
        }
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps, ct: default);
        var hubAppModifier = await appsModCategory.AddOrUpdateModifier
        (
            appModel.PublicKey,
            appModel.ID.ToString(),
            appModel.AppKey.Format(),
            ct: default
        );
        return hubAppModifier.ToModel();
    }

    public static Task<EfApp> HubApp(this IServiceProvider sp)
    {
        var factory = sp.GetRequiredService<EfHubDB>();
        return factory.Apps.App(HubInfo.AppKey, ct: default);
    }

}
