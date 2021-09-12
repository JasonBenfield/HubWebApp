using NUnit.Framework;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.AppRegistration;

namespace HubWebApp.Tests
{
    sealed class NewVersionTest
    {
        [Test]
        public async Task ShouldCreateNewPatch()
        {
            var tester = await setup();
            var model = new NewVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionType = AppVersionType.Values.Patch
            };
            var adminUser = await tester.AdminUser();
            var newVersion = await tester.Execute(model, adminUser);
            Assert.That(newVersion.Status, Is.EqualTo(AppVersionStatus.Values.New));
            Assert.That(newVersion.VersionType, Is.EqualTo(AppVersionType.Values.Patch));
        }

        [Test]
        public async Task ShouldCreateNewMinorVersion()
        {
            var tester = await setup();
            var model = new NewVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionType = AppVersionType.Values.Minor
            };
            var adminUser = await tester.AdminUser();
            var newVersion = await tester.Execute(model, adminUser);
            Assert.That(newVersion.VersionType, Is.EqualTo(AppVersionType.Values.Minor), "Should start new minor version");
        }

        [Test]
        public async Task ShouldCreateNewMajorVersion()
        {
            var tester = await setup();
            var model = new NewVersionRequest
            {
                AppKey = HubInfo.AppKey,
                VersionType = AppVersionType.Values.Major
            };
            var adminUser = await tester.AdminUser();
            var newVersion = await tester.Execute(model, adminUser);
            Assert.That(newVersion.VersionType, Is.EqualTo(AppVersionType.Values.Major), "Should start new major version");
        }

        private async Task<HubActionTester<NewVersionRequest, AppVersionModel>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.AppRegistration.NewVersion);
        }
    }
}
