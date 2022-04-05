using HubWebApp.Fakes;
using XTI_Core.Extensions;
using XTI_HubSetup;

namespace HubWebApp.Tests;

internal sealed class HubSetupTest
{
    [Test]
    public async Task ShouldAddUnknownApp()
    {
        var services = setup();
        var hubSetup = services.GetRequiredService<HubAppSetup>();
        await hubSetup.Run(AppVersionKey.Current);
        var factory = services.GetRequiredService<AppFactory>();
        var unknownApp = await factory.Apps.App(AppKey.Unknown);
        Assert.That(unknownApp.ID.IsValid(), Is.True, "Should add unknown app");
    }

    [Test]
    public async Task ShouldAddHubApp()
    {
        var services = setup();
        var hubSetup = services.GetRequiredService<HubAppSetup>();
        await hubSetup.Run(AppVersionKey.Current);
        var factory = services.GetRequiredService<AppFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        Assert.That(hubApp.Key(), Is.EqualTo(HubInfo.AppKey), "Should add hub app");
    }

    [Test]
    public async Task ShouldAddModCategoryForApps()
    {
        var services = setup();
        var hubSetup = services.GetRequiredService<HubAppSetup>();
        await hubSetup.Run(AppVersionKey.Current);
        var factory = services.GetRequiredService<AppFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var modCategoryName = HubInfo.ModCategories.Apps;
        var modCategory = await hubApp.ModCategory(modCategoryName);
        Assert.That(modCategory.Name(), Is.EqualTo(modCategoryName), "Should add mod category for apps");
    }

    [Test]
    public async Task ShouldAddModifierForEachApp()
    {
        var services = setup();
        var hubSetup = services.GetRequiredService<HubAppSetup>();
        await hubSetup.Run(AppVersionKey.Current);
        var factory = services.GetRequiredService<AppFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var modCategoryName = HubInfo.ModCategories.Apps;
        var modCategory = await hubApp.ModCategory(modCategoryName);
        var modifiers = await modCategory.Modifiers();
        var apps = (await factory.Apps.All()).Where(a => !a.Key().Equals(AppKey.Unknown));
        var modIDs = modifiers.Select(m => m.TargetKey);
        var appIDs = apps.Select(a => a.ID.Value.ToString());
        Assert.That(modIDs, Is.EquivalentTo(appIDs), "Should add modifier for each app");
    }

    private IServiceProvider setup()
    {
        var builder = new XtiHostBuilder();
        builder.Services.AddFakesForHubWebApp();
        return builder.Build().Scope();
    }
}