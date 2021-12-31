using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using XTI_App.Abstractions;
using XTI_Hub;

namespace HubWebApp.Tests;

internal sealed class GetCurrentVersionTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(AppVersionKey.Current.Value);
    }

    [Test]
    public async Task ShouldGetCurrentVersion()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var hubAppModifier = await tester.HubAppModifier();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var currentVersion = await hubApp.CurrentVersion();
        var currentVersionModel = await tester.Execute(AppVersionKey.Current.Value, hubAppModifier.ModKey());
        Assert.That(currentVersionModel?.ID, Is.EqualTo(currentVersion.ID.Value), "Should get current version");
        var version = currentVersion.Version();
        Assert.That(currentVersionModel?.Major, Is.EqualTo(version.Major), "Should get current version");
        Assert.That(currentVersionModel?.Minor, Is.EqualTo(version.Minor), "Should get current version");
        Assert.That(currentVersionModel?.Patch, Is.EqualTo(version.Build), "Should get current version");
        Assert.That(currentVersionModel?.VersionType, Is.EqualTo(currentVersion.Type()), "Should get current version");
        Assert.That(currentVersionModel?.Status, Is.EqualTo(AppVersionStatus.Values.Current), "Should get current version");
    }

    private async Task<HubActionTester<string, AppVersionModel>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Version.GetVersion);
    }
}