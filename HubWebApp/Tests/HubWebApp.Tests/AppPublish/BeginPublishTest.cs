using XTI_Hub.Abstractions;
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
            VersionName = "HubWebApp",
            VersionType = AppVersionType.Values.Patch,
            AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "webapps.example.com") }
        });
        await hubApi.Install.RegisterApp.Invoke(new RegisterAppRequest
        {
            AppTemplate = apiFactory.CreateTemplate().ToModel(),
            Domain = "webapps.example.com",
            VersionKey = version.VersionKey,
            Versions = new XtiVersionModel[0]
        });
        tester.LoginAsAdmin();
        var request = new PublishVersionRequest
        {
            VersionName = version.VersionName,
            VersionKey = version.VersionKey
        };
        await tester.Execute(request);
        version = await hubApi.Install.GetVersion.Invoke(new GetVersionRequest
        {
            VersionName = version.VersionName,
            VersionKey = version.VersionKey
        });
        Assert.That(version.Status, Is.EqualTo(AppVersionStatus.Values.Publishing));
    }

    [Test]
    public async Task ShouldAssignVersionNumber_WhenPatchBeginsPublishing()
    {
        var tester = await setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var patch = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Patch,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        patch = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = patch.VersionName, VersionKey = patch.VersionKey }
        );
        Assert.That(patch.VersionNumber.Major, Is.EqualTo(1), "Should assign version number for new patch");
        Assert.That(patch.VersionNumber.Minor, Is.EqualTo(0), "Should assign version number for new patch");
        Assert.That(patch.VersionNumber.Patch, Is.EqualTo(1), "Should assign version number for new patch");
    }

    [Test]
    public async Task ShouldAssignVersionNumber_WhenMinorVersionBeginsPublishing()
    {
        var tester = await setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var minorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Minor,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        minorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = minorVersion.VersionName, VersionKey = minorVersion.VersionKey }
        );
        Assert.That(minorVersion.VersionNumber.Major, Is.EqualTo(1), "Should assign version number for new minor version");
        Assert.That(minorVersion.VersionNumber.Minor, Is.EqualTo(1), "Should assign version number for new minor version");
        Assert.That(minorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should assign version number for new minor version");
    }

    [Test]
    public async Task ShouldAssignVersionNumber_WhenMajorVersionBeginsPublishing()
    {
        var tester = await setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var majorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Major,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        majorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = majorVersion.VersionName, VersionKey = majorVersion.VersionKey }
        );
        Assert.That(majorVersion.VersionNumber.Major, Is.EqualTo(2), "Should assign version number for new major version");
        Assert.That(majorVersion.VersionNumber.Minor, Is.EqualTo(0), "Should assign version number for new major version");
        Assert.That(majorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should assign version number for new major version");
    }

    [Test]
    public async Task ShouldIncrementPatchOfCurrent_WhenPatchBeginsPublishing()
    {
        var tester = await setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var originalCurrent = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Patch,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = originalCurrent.VersionName, VersionKey = originalCurrent.VersionKey }
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest { VersionName = originalCurrent.VersionName, VersionKey = originalCurrent.VersionKey }
        );
        var patch = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Patch,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        patch = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = patch.VersionName, VersionKey = patch.VersionKey }
        );
        Assert.That(patch.VersionNumber.Major, Is.EqualTo(1), "Should increment patch of current version");
        Assert.That(patch.VersionNumber.Minor, Is.EqualTo(0), "Should increment patch of current version");
        Assert.That(patch.VersionNumber.Patch, Is.EqualTo(2), "Should increment patch of current version");
    }

    [Test]
    public async Task ShouldIncrementMinorVersionOfCurrent_WhenMinorVersionBeginsPublishing()
    {
        var tester = await setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var originalCurrent = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Minor,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = originalCurrent.VersionName, VersionKey = originalCurrent.VersionKey }
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest { VersionName = originalCurrent.VersionName, VersionKey = originalCurrent.VersionKey }
        );
        var minorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Minor,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        minorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = minorVersion.VersionName, VersionKey = minorVersion.VersionKey }
        );
        Assert.That(minorVersion.VersionNumber.Major, Is.EqualTo(1), "Should increment minor of current version");
        Assert.That(minorVersion.VersionNumber.Minor, Is.EqualTo(2), "Should increment minor of current version");
        Assert.That(minorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should increment minor of current version");
    }

    [Test]
    public async Task ShouldIncrementMajorVersionOfCurrent_WhenMajorVersionBeginsPublishing()
    {
        var tester = await setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var originalCurrent = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Major,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = originalCurrent.VersionName, VersionKey = originalCurrent.VersionKey }
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest { VersionName = originalCurrent.VersionName, VersionKey = originalCurrent.VersionKey }
        );
        var majorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Major,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        majorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = majorVersion.VersionName, VersionKey = majorVersion.VersionKey }
        );
        Assert.That(majorVersion.VersionNumber.Major, Is.EqualTo(3), "Should increment major of current version");
        Assert.That(majorVersion.VersionNumber.Minor, Is.EqualTo(0), "Should increment major of current version");
        Assert.That(majorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should increment major of current version");
    }

    [Test]
    public async Task ShouldRetainMajorVersion_WhenMinorVersionBeginsPublishing()
    {
        var tester = await setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var majorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Major,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = majorVersion.VersionName, VersionKey = majorVersion.VersionKey }
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest { VersionName = majorVersion.VersionName, VersionKey = majorVersion.VersionKey }
        );
        var minorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Minor,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        minorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = minorVersion.VersionName, VersionKey = minorVersion.VersionKey }
        );
        Assert.That(minorVersion.VersionNumber.Major, Is.EqualTo(2), "Should retain major version from the previous current");
        Assert.That(minorVersion.VersionNumber.Minor, Is.EqualTo(1), "Should increment minor version");
        Assert.That(minorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should reset patch");
    }

    [Test]
    public async Task ShouldRetainMajorAndMinorVersion_WhenPatchBeginsPublishing()
    {
        var tester = await setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var majorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Major,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = majorVersion.VersionName, VersionKey = majorVersion.VersionKey }
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest { VersionName = majorVersion.VersionName, VersionKey = majorVersion.VersionKey }
        );
        var minorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Minor,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = minorVersion.VersionName, VersionKey = minorVersion.VersionKey }
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest { VersionName = minorVersion.VersionName, VersionKey = minorVersion.VersionKey }
        );
        var patch = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Patch,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        patch = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = patch.VersionName, VersionKey = patch.VersionKey }
        );
        Assert.That(patch.VersionNumber.Major, Is.EqualTo(2), "Should retain major version from the previous current");
        Assert.That(patch.VersionNumber.Minor, Is.EqualTo(1), "Should retain minor version from the previous current");
        Assert.That(patch.VersionNumber.Patch, Is.EqualTo(1), "Should increment patch");
    }

    [Test]
    public async Task ShouldResetPatch_WhenMinorVersionBeginsPublishing()
    {
        var tester = await setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var patch = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Patch,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = patch.VersionName, VersionKey = patch.VersionKey }
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest { VersionName = patch.VersionName, VersionKey = patch.VersionKey }
        );
        var minorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Minor,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        minorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = minorVersion.VersionName, VersionKey = minorVersion.VersionKey }
        );
        Assert.That(minorVersion.VersionNumber.Major, Is.EqualTo(1), "Should reset patch when minor version is publishing");
        Assert.That(minorVersion.VersionNumber.Minor, Is.EqualTo(1), "Should reset patch when minor version is publishing");
        Assert.That(minorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should reset patch when minor version is publishing");
    }

    [Test]
    public async Task ShouldResetPatchAndMinorVersion_WhenMajorVersionBeginsPublishing()
    {
        var tester = await setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var patch = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Patch,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = patch.VersionName, VersionKey = patch.VersionKey }
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest { VersionName = patch.VersionName, VersionKey = patch.VersionKey }
        );
        var minorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Minor,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = minorVersion.VersionName, VersionKey = minorVersion.VersionKey }
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest { VersionName = minorVersion.VersionName, VersionKey = minorVersion.VersionKey }
        );
        var majorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            {
                VersionName = "HubWebApp",
                VersionType = AppVersionType.Values.Major,
                AppDefinitions = new[] { new AppDefinitionModel(HubInfo.AppKey, "hub.example.com") }
            }
        );
        majorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest { VersionName = majorVersion.VersionName, VersionKey = majorVersion.VersionKey }
        );
        Assert.That(majorVersion.VersionNumber.Major, Is.EqualTo(2), "Should reset minor version and patch when major version is publishing");
        Assert.That(majorVersion.VersionNumber.Minor, Is.EqualTo(0), "Should reset minor version and patch when major version is publishing");
        Assert.That(majorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should reset minor version and patch when major version is publishing");
    }

    private async Task<HubActionTester<PublishVersionRequest, XtiVersionModel>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Publish.BeginPublish);
    }
}