using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.AppInstall;
using XTI_HubAppApi.AppPublish;

namespace HubWebApp.Tests;

internal sealed class BeginPublishTest
{
    [Test]
    public async Task ShouldSetStatusToPublishing()
    {
        var tester = await setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var version = await hubApi.Publish.NewVersion.Invoke(new NewVersionRequest
        {
            AppKey = HubInfo.AppKey,
            VersionType = AppVersionType.Values.Patch
        });
        await hubApi.Install.RegisterApp.Invoke(new RegisterAppRequest
        {
            AppTemplate = apiFactory.CreateTemplate().ToModel(),
            VersionKey = version.VersionKey,
            Versions = new AppVersionModel[0]
        });
        var adminUser = await tester.AdminUser();
        var versionKey = AppVersionKey.Parse(version.VersionKey);
        var request = new PublishVersionRequest
        {
            AppKey = HubInfo.AppKey,
            VersionKey = versionKey
        };
        await tester.Execute(request, adminUser);
        version = await hubApi.Install.GetVersion.Invoke(new GetVersionRequest
        {
            AppKey = HubInfo.AppKey,
            VersionKey = versionKey
        });
        Assert.That(version.Status, Is.EqualTo(AppVersionStatus.Values.Publishing));
    }

    [Test]
    public async Task ShouldAssignVersionNumber_WhenPatchBeginsPublishing()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var clock = tester.Services.GetRequiredService<IClock>();
        var patch = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Patch,
            clock.Now()
        );
        await patch.Publishing();
        var versionNumber = patch.Version();
        Assert.That(versionNumber.Major, Is.EqualTo(1), "Should assign version number for new patch");
        Assert.That(versionNumber.Minor, Is.EqualTo(0), "Should assign version number for new patch");
        Assert.That(versionNumber.Build, Is.EqualTo(1), "Should assign version number for new patch");
    }

    [Test]
    public async Task ShouldAssignVersionNumber_WhenMinorVersionBeginsPublishing()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var clock = tester.Services.GetRequiredService<IClock>();
        var minorVersion = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Minor,
            clock.Now()
        );
        await minorVersion.Publishing();
        var versionNumber = minorVersion.Version();
        Assert.That(versionNumber.Major, Is.EqualTo(1), "Should assign version number for new minor version");
        Assert.That(versionNumber.Minor, Is.EqualTo(1), "Should assign version number for new minor version");
        Assert.That(versionNumber.Build, Is.EqualTo(0), "Should assign version number for new minor version");
    }

    [Test]
    public async Task ShouldAssignVersionNumber_WhenMajorVersionBeginsPublishing()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var clock = tester.Services.GetRequiredService<IClock>();
        var majorVersion = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Major,
            clock.Now()
        );
        await majorVersion.Publishing();
        var versionNumber = majorVersion.Version();
        Assert.That(versionNumber.Major, Is.EqualTo(2), "Should assign version number for new major version");
        Assert.That(versionNumber.Minor, Is.EqualTo(0), "Should assign version number for new major version");
        Assert.That(versionNumber.Build, Is.EqualTo(0), "Should assign version number for new major version");
    }

    [Test]
    public async Task ShouldIncrementPatchOfCurrent_WhenPatchBeginsPublishing()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var clock = tester.Services.GetRequiredService<IClock>();
        var originalCurrent = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Patch,
            clock.Now()
        );
        await originalCurrent.Publishing();
        await originalCurrent.Published();
        var patch = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Patch,
            clock.Now()
        );
        await patch.Publishing();
        var versionNumber = patch.Version();
        Assert.That(versionNumber.Major, Is.EqualTo(1), "Should increment patch of current version");
        Assert.That(versionNumber.Minor, Is.EqualTo(0), "Should increment patch of current version");
        Assert.That(versionNumber.Build, Is.EqualTo(2), "Should increment patch of current version");
    }

    [Test]
    public async Task ShouldIncrementMinorVersionOfCurrent_WhenMinorVersionBeginsPublishing()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var clock = tester.Services.GetRequiredService<IClock>();
        var originalCurrent = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Minor,
            clock.Now()
        );
        await originalCurrent.Publishing();
        await originalCurrent.Published();
        var minorVersion = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Minor,
            clock.Now()
        );
        await minorVersion.Publishing();
        var versionNumber = minorVersion.Version();
        Assert.That(versionNumber.Major, Is.EqualTo(1), "Should increment minor of current version");
        Assert.That(versionNumber.Minor, Is.EqualTo(2), "Should increment minor of current version");
        Assert.That(versionNumber.Build, Is.EqualTo(0), "Should increment minor of current version");
    }

    [Test]
    public async Task ShouldIncrementMajorVersionOfCurrent_WhenMajorVersionBeginsPublishing()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var clock = tester.Services.GetRequiredService<IClock>();
        var originalCurrent = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Major,
            clock.Now()
        );
        await originalCurrent.Publishing();
        await originalCurrent.Published();
        var majorVersion = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Major,
            clock.Now()
        );
        await majorVersion.Publishing();
        var versionNumber = majorVersion.Version();
        Assert.That(versionNumber.Major, Is.EqualTo(3), "Should increment major of current version");
        Assert.That(versionNumber.Minor, Is.EqualTo(0), "Should increment major of current version");
        Assert.That(versionNumber.Build, Is.EqualTo(0), "Should increment major of current version");
    }

    [Test]
    public async Task ShouldRetainMajorVersion_WhenMinorVersionBeginsPublishing()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var clock = tester.Services.GetRequiredService<IClock>();
        var majorVersion = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Major,
            clock.Now()
        );
        await majorVersion.Publishing();
        await majorVersion.Published();
        var minorVersion = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Minor,
            clock.Now()
        );
        await minorVersion.Publishing();
        await minorVersion.Published();
        var versionNumber = minorVersion.Version();
        Assert.That(versionNumber.Major, Is.EqualTo(2), "Should retain major version from the previous current");
        Assert.That(versionNumber.Minor, Is.EqualTo(1), "Should increment minor version");
        Assert.That(versionNumber.Build, Is.EqualTo(0), "Should reset patch");
    }

    [Test]
    public async Task ShouldRetainMajorAndMinorVersion_WhenPatchBeginsPublishing()
    {
        var tester = await setup();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var clock = tester.Services.GetRequiredService<IClock>();
        var majorVersion = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Major,
            clock.Now()
        );
        await majorVersion.Publishing();
        await majorVersion.Published();
        var minorVersion = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Minor,
            clock.Now()
        );
        await minorVersion.Publishing();
        await minorVersion.Published();
        var patch = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Patch,
            clock.Now()
        );
        await patch.Publishing();
        var versionNumber = patch.Version();
        Assert.That(versionNumber.Major, Is.EqualTo(2), "Should retain major version from the previous current");
        Assert.That(versionNumber.Minor, Is.EqualTo(1), "Should retain minor version from the previous current");
        Assert.That(versionNumber.Build, Is.EqualTo(1), "Should increment patch");
    }

    [Test]
    public async Task ShouldResetPatch_WhenMinorVersionBeginsPublishing()
    {
        var tester = await setup();
        var clock = tester.Services.GetRequiredService<IClock>();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var patch = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Patch,
            clock.Now()
        );
        await patch.Publishing();
        await patch.Published();
        var minorVersion = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Minor,
            clock.Now()
        );
        await minorVersion.Publishing();
        var versionNumber = minorVersion.Version();
        Assert.That(versionNumber.Major, Is.EqualTo(1), "Should reset patch when minor version is publishing");
        Assert.That(versionNumber.Minor, Is.EqualTo(1), "Should reset patch when minor version is publishing");
        Assert.That(versionNumber.Build, Is.EqualTo(0), "Should reset patch when minor version is publishing");
    }

    [Test]
    public async Task ShouldResetPatchAndMinorVersion_WhenMajorVersionBeginsPublishing()
    {
        var tester = await setup();
        var clock = tester.Services.GetRequiredService<IClock>();
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var patch = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Patch,
            clock.Now()
        );
        await patch.Publishing();
        await patch.Published();
        var minorVersion = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Minor,
            clock.Now()
        );
        await minorVersion.Publishing();
        await minorVersion.Published();
        var majorVersion = await factory.Apps.StartNewVersion
        (
            HubInfo.AppKey,
            AppVersionType.Values.Major,
            clock.Now()
        );
        await majorVersion.Publishing();
        var versionNumber = majorVersion.Version();
        Assert.That(versionNumber.Major, Is.EqualTo(2), "Should reset minor version and patch when major version is publishing");
        Assert.That(versionNumber.Minor, Is.EqualTo(0), "Should reset minor version and patch when major version is publishing");
        Assert.That(versionNumber.Build, Is.EqualTo(0), "Should reset minor version and patch when major version is publishing");
    }

    private async Task<HubActionTester<PublishVersionRequest, AppVersionModel>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Publish.BeginPublish);
    }
}