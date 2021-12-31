using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using XTI_App.Abstractions;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.AppInstall;
using XTI_HubAppApi.AppPublish;

namespace HubWebApp.Tests;

internal sealed class EndPublishTest
{
    [Test]
    public async Task ShouldSetStatusToCurrent_WhenPublished()
    {
        var tester = await setup();
        var hubApiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = hubApiFactory.CreateForSuperUser();
        var newVersion = await hubApi.Publish.NewVersion.Invoke(new NewVersionRequest
        {
            AppKey = HubInfo.AppKey,
            VersionType = AppVersionType.Values.Patch
        });
        var request = new PublishVersionRequest
        {
            AppKey = HubInfo.AppKey,
            VersionKey = AppVersionKey.Parse(newVersion.VersionKey)
        };
        await hubApi.Publish.BeginPublish.Invoke(request);
        tester.LoginAsAdmin();
        var version = await tester.Execute(request);
        Assert.That(version.Status, Is.EqualTo(AppVersionStatus.Values.Current), "Should set status to current when published");
    }

    [Test]
    public async Task ShouldOnlyAllowOneCurrentVersion()
    {
        var tester = await setup();
        var hubApiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = hubApiFactory.CreateForSuperUser();
        var version1 = await hubApi.Publish.NewVersion.Invoke(new NewVersionRequest
        {
            AppKey = HubInfo.AppKey,
            VersionType = AppVersionType.Values.Patch
        });
        await hubApi.Publish.BeginPublish.Invoke(new PublishVersionRequest
        {
            AppKey = HubInfo.AppKey,
            VersionKey = AppVersionKey.Parse(version1.VersionKey)
        });
        await hubApi.Publish.EndPublish.Invoke(new PublishVersionRequest
        {
            AppKey = HubInfo.AppKey,
            VersionKey = AppVersionKey.Parse(version1.VersionKey)
        });

        var version2 = await hubApi.Publish.NewVersion.Invoke(new NewVersionRequest
        {
            AppKey = HubInfo.AppKey,
            VersionType = AppVersionType.Values.Patch
        });
        await hubApi.Publish.BeginPublish.Invoke(new PublishVersionRequest
        {
            AppKey = HubInfo.AppKey,
            VersionKey = AppVersionKey.Parse(version2.VersionKey)
        });
        await hubApi.Publish.EndPublish.Invoke(new PublishVersionRequest
        {
            AppKey = HubInfo.AppKey,
            VersionKey = AppVersionKey.Parse(version2.VersionKey)
        });
        version1 = await hubApi.Install.GetVersion.Invoke(new GetVersionRequest
        {
            AppKey = HubInfo.AppKey,
            VersionKey = AppVersionKey.Parse(version1.VersionKey)
        });
        version2 = await hubApi.Install.GetVersion.Invoke(new GetVersionRequest
        {
            AppKey = HubInfo.AppKey,
            VersionKey = AppVersionKey.Parse(version2.VersionKey)
        });
        Assert.That(version1.Status, Is.EqualTo(AppVersionStatus.Values.Old), "Should archive previous version");
        Assert.That(version2.Status, Is.EqualTo(AppVersionStatus.Values.Current), "Should make latest published version current");
    }

    private async Task<HubActionTester<PublishVersionRequest, AppVersionModel>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Publish.EndPublish);
    }
}