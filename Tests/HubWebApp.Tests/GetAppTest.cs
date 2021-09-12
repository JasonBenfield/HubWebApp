using XTI_HubAppApi;
using NUnit.Framework;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace HubWebApp.Tests
{
    public sealed class GetAppTest
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
        public async Task ShouldThrowError_WhenModifierIsNotAssignedToUser()
        {
            var tester = await setup();
            var app = await tester.HubApp();
            var viewAppRole = await app.Role(new AppRoleName(HubInfo.Roles.ViewApp));
            var modifier = await tester.HubAppModifier();
            await AccessAssertions.Create(tester)
                .ShouldThrowError_WhenModifierIsNotAssignedToUser_ButRoleIsAssignedToUser
                (
                    new EmptyRequest(),
                    viewAppRole,
                    modifier
                );
        }

        [Test]
        public async Task ShouldGetApp()
        {
            var tester = await setup();
            var adminUser = await tester.AdminUser();
            var adminRole = await tester.AdminRole();
            var hubAppModifier = await tester.HubAppModifier();
            await adminUser.AddRole(adminRole, hubAppModifier);
            var app = await tester.Execute(new EmptyRequest(), adminUser, hubAppModifier.ModKey());
            Assert.That(app?.Title, Is.EqualTo("Hub"), "Should get app");
        }

        private async Task<HubActionTester<EmptyRequest, AppModel>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.App.GetApp);
        }
    }
}
