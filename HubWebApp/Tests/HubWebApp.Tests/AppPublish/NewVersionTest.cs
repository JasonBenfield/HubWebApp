using Microsoft.EntityFrameworkCore;
using XTI_Hub.Abstractions;
using XTI_HubAppApi.AppPublish;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

sealed class NewVersionTest
{
    [Test]
    public async Task ShouldSetDomainForWebApps_WhenStartingNewVersion()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var model = new NewVersionRequest
        {
            VersionName = "HubWebApp",
            VersionType = AppVersionType.Values.Patch,
            AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "webapps.xartogg.com") }
        };
        var newVersion = await tester.Execute(model);
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var version = await db.AppVersions.Retrieve()
            .FirstAsync(v => v.VersionID == newVersion.ID);
        Assert.That(version.Domain, Is.EqualTo(model.AppDefinitions[0].Domain), "Should set domain when starting a new version");
    }

    [Test]
    public async Task ShouldCreateNewPatch()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var model = new NewVersionRequest
        {
            VersionName = "HubWebApp",
            VersionType = AppVersionType.Values.Patch,
            AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "webapps.xartogg.com") }
        };
        var newVersion = await tester.Execute(model);
        Assert.That(newVersion.Status, Is.EqualTo(AppVersionStatus.Values.New));
        Assert.That(newVersion.VersionType, Is.EqualTo(AppVersionType.Values.Patch));
    }

    [Test]
    public async Task ShouldCreateNewMinorVersion()
    {
        var tester = await setup();
        var model = new NewVersionRequest
        {
            VersionName = "HubWebApp",
            VersionType = AppVersionType.Values.Minor,
            AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "webapps.xartogg.com") }
        };
        tester.LoginAsAdmin();
        var newVersion = await tester.Execute(model);
        Assert.That(newVersion.VersionType, Is.EqualTo(AppVersionType.Values.Minor), "Should start new minor version");
    }

    [Test]
    public async Task ShouldCreateNewMajorVersion()
    {
        var tester = await setup();
        var model = new NewVersionRequest
        {
            VersionName = "HubWebApp",
            VersionType = AppVersionType.Values.Major,
            AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "webapps.xartogg.com") }
        };
        tester.LoginAsAdmin();
        var newVersion = await tester.Execute(model);
        Assert.That(newVersion.VersionType, Is.EqualTo(AppVersionType.Values.Major), "Should start new major version");
    }

    private async Task<HubActionTester<NewVersionRequest, XtiVersionModel>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Publish.NewVersion);
    }
}