namespace HubWebApp.Tests;

internal sealed class BeginPublishTest
{
    [Test]
    public async Task ShouldSetStatusToPublishing()
    {
        var tester = await Setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var version = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Patch
            )
        );
        await hubApi.Install.RegisterApp.Invoke
        (
            new RegisterAppRequest
            (
                appTemplate: apiFactory.CreateTemplate().ToModel(),
                versionKey: version.VersionKey
            )
        );
        await tester.LoginAsAdmin();
        await tester.Execute
        (
            new PublishVersionRequest
            (
                versionName: version.VersionName,
                versionKey: version.VersionKey
            )
        );
        version = await hubApi.Install.GetVersion.Invoke
        (
            new GetVersionRequest
            (
                versionName: version.VersionName,
                versionKey: version.VersionKey
            )
        );
        Assert.That(version.Status, Is.EqualTo(AppVersionStatus.Values.Publishing));
    }

    [Test]
    public async Task ShouldAssignVersionNumber_WhenPatchBeginsPublishing()
    {
        var tester = await Setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var patch = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Patch
            )
        );
        patch = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: patch.VersionName,
                versionKey: patch.VersionKey
            )
        );
        Assert.That(patch.VersionNumber.Major, Is.EqualTo(1), "Should assign version number for new patch");
        Assert.That(patch.VersionNumber.Minor, Is.EqualTo(0), "Should assign version number for new patch");
        Assert.That(patch.VersionNumber.Patch, Is.EqualTo(1), "Should assign version number for new patch");
    }

    [Test]
    public async Task ShouldAssignVersionNumber_WhenMinorVersionBeginsPublishing()
    {
        var tester = await Setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var minorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Minor
            )
        );
        minorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: minorVersion.VersionName,
                versionKey: minorVersion.VersionKey
            )
        );
        Assert.That(minorVersion.VersionNumber.Major, Is.EqualTo(1), "Should assign version number for new minor version");
        Assert.That(minorVersion.VersionNumber.Minor, Is.EqualTo(1), "Should assign version number for new minor version");
        Assert.That(minorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should assign version number for new minor version");
    }

    [Test]
    public async Task ShouldAssignVersionNumber_WhenMajorVersionBeginsPublishing()
    {
        var tester = await Setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var majorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Major
            )
        );
        majorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: majorVersion.VersionName,
                versionKey: majorVersion.VersionKey
            )
        );
        Assert.That(majorVersion.VersionNumber.Major, Is.EqualTo(2), "Should assign version number for new major version");
        Assert.That(majorVersion.VersionNumber.Minor, Is.EqualTo(0), "Should assign version number for new major version");
        Assert.That(majorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should assign version number for new major version");
    }

    [Test]
    public async Task ShouldIncrementPatchOfCurrent_WhenPatchBeginsPublishing()
    {
        var tester = await Setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var originalCurrent = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Patch
            )
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: originalCurrent.VersionName,
                versionKey: originalCurrent.VersionKey
            )
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: originalCurrent.VersionName,
                versionKey: originalCurrent.VersionKey
            )
        );
        var patch = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Patch
            )
        );
        patch = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: patch.VersionName,
                versionKey: patch.VersionKey
            )
        );
        Assert.That(patch.VersionNumber.Major, Is.EqualTo(1), "Should increment patch of current version");
        Assert.That(patch.VersionNumber.Minor, Is.EqualTo(0), "Should increment patch of current version");
        Assert.That(patch.VersionNumber.Patch, Is.EqualTo(2), "Should increment patch of current version");
    }

    [Test]
    public async Task ShouldIncrementMinorVersionOfCurrent_WhenMinorVersionBeginsPublishing()
    {
        var tester = await Setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var originalCurrent = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Minor
            )
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: originalCurrent.VersionName, 
                versionKey: originalCurrent.VersionKey 
            )
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: originalCurrent.VersionName,
                versionKey: originalCurrent.VersionKey
            )
        );
        var minorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Minor
            )
        );
        minorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: minorVersion.VersionName,
                versionKey: minorVersion.VersionKey
            )
        );
        Assert.That(minorVersion.VersionNumber.Major, Is.EqualTo(1), "Should increment minor of current version");
        Assert.That(minorVersion.VersionNumber.Minor, Is.EqualTo(2), "Should increment minor of current version");
        Assert.That(minorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should increment minor of current version");
    }

    [Test]
    public async Task ShouldIncrementMajorVersionOfCurrent_WhenMajorVersionBeginsPublishing()
    {
        var tester = await Setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var originalCurrent = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Major
            )
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: originalCurrent.VersionName,
                versionKey: originalCurrent.VersionKey
            )
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: originalCurrent.VersionName,
                versionKey: originalCurrent.VersionKey
            )
        );
        var majorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Major
            )
        );
        majorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: majorVersion.VersionName,
                versionKey: majorVersion.VersionKey
            )
        );
        Assert.That(majorVersion.VersionNumber.Major, Is.EqualTo(3), "Should increment major of current version");
        Assert.That(majorVersion.VersionNumber.Minor, Is.EqualTo(0), "Should increment major of current version");
        Assert.That(majorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should increment major of current version");
    }

    [Test]
    public async Task ShouldRetainMajorVersion_WhenMinorVersionBeginsPublishing()
    {
        var tester = await Setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var majorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Major
            )
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: majorVersion.VersionName,
                versionKey: majorVersion.VersionKey
            )
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: majorVersion.VersionName,
                versionKey: majorVersion.VersionKey
            )
        );
        var minorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Minor
            )
        );
        minorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: minorVersion.VersionName,
                versionKey: minorVersion.VersionKey
            )
        );
        Assert.That(minorVersion.VersionNumber.Major, Is.EqualTo(2), "Should retain major version from the previous current");
        Assert.That(minorVersion.VersionNumber.Minor, Is.EqualTo(1), "Should increment minor version");
        Assert.That(minorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should reset patch");
    }

    [Test]
    public async Task ShouldRetainMajorAndMinorVersion_WhenPatchBeginsPublishing()
    {
        var tester = await Setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var majorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Major
            )
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: majorVersion.VersionName,
                versionKey: majorVersion.VersionKey
            )
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: majorVersion.VersionName,
                versionKey: majorVersion.VersionKey
            )
        );
        var minorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Minor
            )
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: minorVersion.VersionName,
                versionKey: minorVersion.VersionKey
            )
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: minorVersion.VersionName,
                versionKey: minorVersion.VersionKey
            )
        );
        var patch = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Patch
            )
        );
        patch = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: patch.VersionName,
                versionKey: patch.VersionKey
            )
        );
        Assert.That(patch.VersionNumber.Major, Is.EqualTo(2), "Should retain major version from the previous current");
        Assert.That(patch.VersionNumber.Minor, Is.EqualTo(1), "Should retain minor version from the previous current");
        Assert.That(patch.VersionNumber.Patch, Is.EqualTo(1), "Should increment patch");
    }

    [Test]
    public async Task ShouldResetPatch_WhenMinorVersionBeginsPublishing()
    {
        var tester = await Setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var patch = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Patch
            )
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: patch.VersionName,
                versionKey: patch.VersionKey
            )
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: patch.VersionName,
                versionKey: patch.VersionKey
            )
        );
        var minorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Minor
            )
        );
        minorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: minorVersion.VersionName,
                versionKey: minorVersion.VersionKey
            )
        );
        Assert.That(minorVersion.VersionNumber.Major, Is.EqualTo(1), "Should reset patch when minor version is publishing");
        Assert.That(minorVersion.VersionNumber.Minor, Is.EqualTo(1), "Should reset patch when minor version is publishing");
        Assert.That(minorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should reset patch when minor version is publishing");
    }

    [Test]
    public async Task ShouldResetPatchAndMinorVersion_WhenMajorVersionBeginsPublishing()
    {
        var tester = await Setup();
        var apiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = apiFactory.CreateForSuperUser();
        var patch = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Patch
            )
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: patch.VersionName,
                versionKey: patch.VersionKey
            )
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: patch.VersionName,
                versionKey: patch.VersionKey
            )
        );
        var minorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Minor
            )
        );
        await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: minorVersion.VersionName,
                versionKey: minorVersion.VersionKey
            )
        );
        await hubApi.Publish.EndPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: minorVersion.VersionName,
                versionKey: minorVersion.VersionKey
            )
        );
        var majorVersion = await hubApi.Publish.NewVersion.Invoke
        (
            new NewVersionRequest
            (
                versionName: new AppVersionName("HubWebApp"),
                versionType: AppVersionType.Values.Major
            )
        );
        majorVersion = await hubApi.Publish.BeginPublish.Invoke
        (
            new PublishVersionRequest
            (
                versionName: majorVersion.VersionName,
                versionKey: majorVersion.VersionKey
            )
        );
        Assert.That(majorVersion.VersionNumber.Major, Is.EqualTo(2), "Should reset minor version and patch when major version is publishing");
        Assert.That(majorVersion.VersionNumber.Minor, Is.EqualTo(0), "Should reset minor version and patch when major version is publishing");
        Assert.That(majorVersion.VersionNumber.Patch, Is.EqualTo(0), "Should reset minor version and patch when major version is publishing");
    }

    private async Task<HubActionTester<PublishVersionRequest, XtiVersionModel>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Publish.BeginPublish);
    }
}