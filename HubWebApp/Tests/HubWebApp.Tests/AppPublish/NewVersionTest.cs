﻿using XTI_Hub.Abstractions;
using XTI_HubAppApi.AppPublish;

namespace HubWebApp.Tests;

sealed class NewVersionTest
{
    [Test]
    public async Task ShouldCreateNewPatch()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var model = new NewVersionRequest
        {
            VersionName = new AppVersionName("HubWebApp"),
            VersionType = AppVersionType.Values.Patch,
            AppKeys = new[] { HubInfo.AppKey }
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
            VersionName = new AppVersionName("HubWebApp"),
            VersionType = AppVersionType.Values.Minor,
            AppKeys = new[] { HubInfo.AppKey }
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
            VersionName = new AppVersionName("HubWebApp"),
            VersionType = AppVersionType.Values.Major,
            AppKeys = new[] { HubInfo.AppKey }
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