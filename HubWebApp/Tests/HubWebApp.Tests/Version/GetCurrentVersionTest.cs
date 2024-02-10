namespace HubWebApp.Tests;

internal sealed class GetCurrentVersionTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(AppVersionKey.Current.Value);
    }

    [Test]
    public async Task ShouldGetCurrentVersion()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var hubAppModifier = await tester.HubAppModifier();
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var currentVersion = await hubApp.CurrentVersion();
        var currentVersionModel = await tester.Execute(AppVersionKey.Current.Value, hubAppModifier.ModKey);
        Assert.That(currentVersionModel?.ID, Is.EqualTo(currentVersion.Version.ToModel().ID), "Should get current version");
        Assert.That(currentVersionModel?.VersionNumber, Is.EqualTo(currentVersion.Version.ToModel().VersionNumber), "Should get current version");
        Assert.That(currentVersionModel?.VersionType, Is.EqualTo(currentVersion.Version.ToModel().VersionType), "Should get current version");
        Assert.That(currentVersionModel?.Status, Is.EqualTo(AppVersionStatus.Values.Current), "Should get current version");
    }

    private async Task<HubActionTester<string, XtiVersionModel>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Version.GetVersion);
    }
}