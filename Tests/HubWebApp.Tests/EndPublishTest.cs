using NUnit.Framework;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_HubAppApi.AppRegistration;
using XTI_HubAppApi;
using Microsoft.Extensions.DependencyInjection;

namespace HubWebApp.Tests
{
    public sealed class EndPublishTest
    {
        [Test]
        public async Task ShouldSetStatusToCurrent_WhenPublished()
        {
            var tester = await setup();
            var hubApiFactory = tester.Services.GetService<HubAppApiFactory>();
            var hubApi = hubApiFactory.CreateForSuperUser();
            var newVersion = await hubApi.AppRegistration.NewVersion.Invoke(new NewVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionType = AppVersionType.Values.Patch
            });
            var request = new GetVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionKey = AppVersionKey.Parse(newVersion.VersionKey)
            };
            await hubApi.AppRegistration.BeginPublish.Invoke(request);
            var adminUser = await tester.AdminUser();
            var version = await tester.Execute(request, adminUser);
            Assert.That(version.Status, Is.EqualTo(AppVersionStatus.Values.Current), "Should set status to current when published");
        }

        [Test]
        public async Task ShouldOnlyAllowOneCurrentVersion()
        {
            var tester = await setup();
            var hubApiFactory = tester.Services.GetService<HubAppApiFactory>();
            var hubApi = hubApiFactory.CreateForSuperUser();
            var version1 = await hubApi.AppRegistration.NewVersion.Invoke(new NewVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionType = AppVersionType.Values.Patch
            });
            await hubApi.AppRegistration.BeginPublish.Invoke(new GetVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionKey = AppVersionKey.Parse(version1.VersionKey)
            });
            await hubApi.AppRegistration.EndPublish.Invoke(new GetVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionKey = AppVersionKey.Parse(version1.VersionKey)
            });

            var version2 = await hubApi.AppRegistration.NewVersion.Invoke(new NewVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionType = AppVersionType.Values.Patch
            });
            await hubApi.AppRegistration.BeginPublish.Invoke(new GetVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionKey = AppVersionKey.Parse(version2.VersionKey)
            });
            await hubApi.AppRegistration.EndPublish.Invoke(new GetVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionKey = AppVersionKey.Parse(version2.VersionKey)
            });
            version1 = await hubApi.AppRegistration.GetVersion.Invoke(new GetVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionKey = AppVersionKey.Parse(version1.VersionKey)
            });
            version2 = await hubApi.AppRegistration.GetVersion.Invoke(new GetVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionKey = AppVersionKey.Parse(version2.VersionKey)
            });
            Assert.That(version1.Status, Is.EqualTo(AppVersionStatus.Values.Old), "Should archive previous version");
            Assert.That(version2.Status, Is.EqualTo(AppVersionStatus.Values.Current), "Should make latest published version current");
        }

        private async Task<HubActionTester<GetVersionRequest, AppVersionModel>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.AppRegistration.EndPublish);
        }
    }
}
