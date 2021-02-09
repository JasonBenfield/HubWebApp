using HubWebAppApi;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Tests
{
    public sealed class AllAppsTest
    {
        [Test]
        public async Task ShouldGetAllApps_WhenUserIsModCategoryAdmin()
        {
            var tester = await setup();
            var adminUser = await tester.AdminUser();
            var hubApp = await tester.HubApp();
            var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            await adminUser.GrantFullAccessToModCategory(appsModCategory);
            var apps = await tester.Execute(new EmptyRequest(), adminUser);
            var appNames = apps.Select(a => a.AppName);
            Assert.That
            (
                appNames,
                Is.EquivalentTo(new[] { AppKey.Unknown.Name.DisplayText, HubInfo.AppKey.Name.DisplayText }),
                "Should get all apps"
            );
        }

        [Test]
        public async Task ShouldOnlyGetAllowedApps()
        {
            var tester = await setup();
            var adminUser = await tester.AdminUser();
            var hubAppModifier = await tester.HubAppModifier();
            await adminUser.AddModifier(hubAppModifier);
            var apps = await tester.Execute(new EmptyRequest(), adminUser);
            var appNames = apps.Select(a => a.AppName);
            Assert.That
            (
                appNames,
                Is.EquivalentTo(new[] { HubInfo.AppKey.Name.DisplayText }),
                "Should get only allowed apps"
            );
        }

        private async Task<HubActionTester<EmptyRequest, AppModel[]>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.Apps.All);
        }
    }
}
