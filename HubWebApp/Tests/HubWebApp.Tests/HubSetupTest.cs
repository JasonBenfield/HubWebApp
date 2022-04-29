using HubWebApp.Fakes;
using XTI_Core.Extensions;
using XTI_Hub.Abstractions;

namespace HubWebApp.Tests;

internal sealed class HubSetupTest
{
    [Test]
    public async Task ShouldAddHubApp()
    {
        var sp = await setup();
        var hubSetup = sp.GetRequiredService<HubAppSetup>();
        await hubSetup.Run(AppVersionKey.Current);
        var factory = sp.GetRequiredService<AppFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        Assert.That(hubApp.Key(), Is.EqualTo(HubInfo.AppKey), "Should add hub app");
    }

    [Test]
    public async Task ShouldAddModCategoryForApps()
    {
        var sp = await setup();
        var hubSetup = sp.GetRequiredService<HubAppSetup>();
        await hubSetup.Run(AppVersionKey.Current);
        var factory = sp.GetRequiredService<AppFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var modCategoryName = HubInfo.ModCategories.Apps;
        var modCategory = await hubApp.ModCategory(modCategoryName);
        Assert.That(modCategory.Name(), Is.EqualTo(modCategoryName), "Should add mod category for apps");
    }

    [Test]
    public async Task ShouldAddModifierForEachApp()
    {
        var sp = await setup();
        var hubSetup = sp.GetRequiredService<HubAppSetup>();
        await hubSetup.Run(AppVersionKey.Current);
        var factory = sp.GetRequiredService<AppFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var modCategoryName = HubInfo.ModCategories.Apps;
        var modCategory = await hubApp.ModCategory(modCategoryName);
        var modifiers = await modCategory.Modifiers();
        var apps = (await factory.Apps.All()).Where(a => !a.Key().Equals(AppKey.Unknown));
        var modIDs = modifiers.Select(m => m.TargetKey);
        var appIDs = apps.Select(a => a.ID.Value.ToString());
        Assert.That(modIDs, Is.EquivalentTo(appIDs), "Should add modifier for each app");
    }

    private async Task<IServiceProvider> setup()
    {
        var builder = new XtiHostBuilder();
        builder.Services.AddFakesForHubWebApp();
        var sp = builder.Build().Scope();
        var initialSetup = sp.GetRequiredService<InitialSetup>();
        await initialSetup.Run();
        var hubAdmin = sp.GetRequiredService<IHubAdministration>();
        await hubAdmin.AddOrUpdateApps
        (
            new AppVersionName("HubWebApp"),
            new[] { new AppDefinitionModel(HubInfo.AppKey) }
        );
        await hubAdmin.AddOrUpdateVersions
        (
            new[] { HubInfo.AppKey },
            new[]
            {
                new XtiVersionModel
                {
                    VersionName = new AppVersionName("HubWebApp"),
                    VersionKey = new AppVersionKey(1),
                    VersionNumber = new AppVersionNumber(1,0,0),
                    Status = AppVersionStatus.Values.Current,
                    VersionType = AppVersionType.Values.Major,
                    TimeAdded = DateTime.Now
                }
            }
        );
        return sp;
    }
}