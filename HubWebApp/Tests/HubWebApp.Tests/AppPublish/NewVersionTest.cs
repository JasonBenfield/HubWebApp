namespace HubWebApp.Tests;

sealed class NewVersionTest
{
    [Test]
    public async Task ShouldCreateNewPatch()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var newVersionRequest = new NewVersionRequest
        (
            versionName: new AppVersionName("HubWebApp"),
            versionType: AppVersionType.Values.Patch
        );
        var newVersion = await tester.Execute(newVersionRequest);
        Assert.That(newVersion.Status, Is.EqualTo(AppVersionStatus.Values.New));
        Assert.That(newVersion.VersionType, Is.EqualTo(AppVersionType.Values.Patch));
    }

    [Test]
    public async Task ShouldCreateNewMinorVersion()
    {
        var tester = await Setup();
        var newVersionRequest = new NewVersionRequest
        (
            versionName: new AppVersionName("HubWebApp"),
            versionType: AppVersionType.Values.Minor
        );
        await tester.LoginAsAdmin();
        var newVersion = await tester.Execute(newVersionRequest);
        Assert.That(newVersion.VersionType, Is.EqualTo(AppVersionType.Values.Minor), "Should start new minor version");
    }

    [Test]
    public async Task ShouldCreateNewMajorVersion()
    {
        var tester = await Setup();
        var newVersionRequest = new NewVersionRequest
        (
            versionName: new AppVersionName("HubWebApp"),
            versionType: AppVersionType.Values.Major
        );
        await tester.LoginAsAdmin();
        var newVersion = await tester.Execute(newVersionRequest);
        Assert.That(newVersion.VersionType, Is.EqualTo(AppVersionType.Values.Major), "Should start new major version");
    }

    private async Task<HubActionTester<NewVersionRequest, XtiVersionModel>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Publish.NewVersion);
    }
}