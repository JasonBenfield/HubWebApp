using HubWebApp.Fakes;
using XTI_Core.Extensions;
using XTI_Internal.Abstractions;

namespace HubWebApp.Tests;

internal sealed class HubSetupTest
{
    [Test]
    public async Task ShouldAddHubApp()
    {
        var sp = await Setup();
        var hubSetup = sp.GetRequiredService<HubAppSetup>();
        await hubSetup.Run(AppVersionKey.Current, ct: default);
        var factory = sp.GetRequiredService<EfHubDB>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey, ct: default);
        Assert.That(hubApp.ToModel().AppKey, Is.EqualTo(HubInfo.AppKey), "Should add hub app");
    }

    [Test]
    public async Task ShouldAddModCategoryForApps()
    {
        var sp = await Setup();
        var hubSetup = sp.GetRequiredService<HubAppSetup>();
        await hubSetup.Run(AppVersionKey.Current, ct: default);
        var factory = sp.GetRequiredService<EfHubDB>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey, ct: default);
        var modCategoryName = HubInfo.ModCategories.Apps;
        var modCategory = await hubApp.ModCategory(modCategoryName, ct: default);
        Assert.That(modCategory.ToModel().Name, Is.EqualTo(modCategoryName), "Should add mod category for apps");
    }

    [Test]
    public async Task ShouldAddModifierForEachApp()
    {
        var sp = await Setup();
        var hubSetup = sp.GetRequiredService<HubAppSetup>();
        await hubSetup.Run(AppVersionKey.Current, ct: default);
        var factory = sp.GetRequiredService<EfHubDB>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey, ct: default);
        var modCategoryName = HubInfo.ModCategories.Apps;
        var modCategory = await hubApp.ModCategory(modCategoryName, ct: default);
        var modifiers = await modCategory.Modifiers(ct: default);
        var apps = await factory.Apps.All(ct: default);
        var modIDs = modifiers.Select(m => m.ToModel().TargetKey);
        var appIDs = apps
            .Select(a => a.ToModel())
            .Where(a => !a.IsUnknown())
            .Select(a => a.ID.ToString())
            .ToArray();
        Assert.That(modIDs, Is.EquivalentTo(appIDs), "Should add modifier for each app");
    }

    private async Task<IServiceProvider> Setup()
    {
        var builder = new XtiHostBuilder();
        builder.Services.AddFakesForHubWebApp();
        var sp = builder.Build().Scope();
        var initialSetup = sp.GetRequiredService<InitialSetup>();
        await initialSetup.Run(ct: default);
        var hubAdmin = sp.GetRequiredService<IHubService>();
        await hubAdmin.AddOrUpdateApps
        (
            new AppVersionName("HubWebApp"),
            "Fake", 
            "Fake",
            [HubInfo.AppKey],
            default
        );
        await hubAdmin.AddOrUpdateVersions
        (
            [HubInfo.AppKey],
            [
                new AddVersionRequest
                (
                    versionName: new AppVersionName("HubWebApp"),
                    versionKey: new AppVersionKey(1),
                    versionNumber: new AppVersionNumber(1,0,0),
                    status: AppVersionStatus.Values.Current,
                    versionType: AppVersionType.Values.Major
                )
            ],
            default
        );
        return sp;
    }
}