using NUnit.Framework;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace HubWebApp.Tests
{
    public sealed class GetCurrentVersionTest
    {
        [Test]
        public async Task ShouldThrowError_WhenModifierIsBlank()
        {
            var tester = await setup();
            await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(new EmptyRequest());
        }

        [Test]
        public async Task ShouldThrowError_WhenModifierIsNotFound()
        {
            var tester = await setup();
            await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsNotFound(new EmptyRequest());
        }

        [Test]
        public async Task ShouldGetCurrentVersion()
        {
            var tester = await setup();
            var adminUser = await tester.AdminUser();
            var hubAppModifier = await tester.HubAppModifier();
            await adminUser.AddModifier(hubAppModifier);
            var hubApp = await tester.HubApp();
            var currentVersion = await hubApp.CurrentVersion();
            var currentVersionModel = await tester.Execute(new EmptyRequest(), adminUser, hubAppModifier.ModKey());
            Assert.That(currentVersionModel?.ID, Is.EqualTo(currentVersion.ID.Value), "Should get current version");
            var version = currentVersion.Version();
            Assert.That(currentVersionModel?.Major, Is.EqualTo(version.Major), "Should get current version");
            Assert.That(currentVersionModel?.Minor, Is.EqualTo(version.Minor), "Should get current version");
            Assert.That(currentVersionModel?.Patch, Is.EqualTo(version.Build), "Should get current version");
            Assert.That(currentVersionModel?.VersionType, Is.EqualTo(currentVersion.Type()), "Should get current version");
            Assert.That(currentVersionModel?.Status, Is.EqualTo(AppVersionStatus.Values.Current), "Should get current version");
        }

        private async Task<HubActionTester<EmptyRequest, AppVersionModel>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.App.GetCurrentVersion);
        }
    }
}
