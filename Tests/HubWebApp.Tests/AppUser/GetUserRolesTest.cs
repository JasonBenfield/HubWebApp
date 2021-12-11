using XTI_HubAppApi;
using NUnit.Framework;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_HubAppApi.AppUserInquiry;
using System;
using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.UserList;
using System.Linq;

namespace HubWebApp.Tests
{
    public sealed class GetUserRolesTest
    {
        [Test]
        public async Task ShouldGetUserRoles()
        {
            var tester = await setup();
            var adminUser = await tester.AdminUser();
            var hubApp = await tester.HubApp();
            var hubAppModifier = await tester.HubAppModifier();
            var adminRole = await tester.AdminRole();
            await adminUser.AddRole(adminRole, hubAppModifier);
            var viewUserRole = await hubApp.Role(HubInfo.Roles.ViewUser);
            await adminUser.AddRole(viewUserRole, hubAppModifier);
            var request = new GetUserRolesRequest
            {
                UserID = adminUser.ID.Value,
                ModifierID = hubAppModifier.ID.Value
            };
            var roles = await tester.Execute(request, adminUser, hubAppModifier.ModKey());
            Assert.That(roles.Select(r => new AppRoleName(r.Name)), Has.One.EqualTo(HubInfo.Roles.ViewUser), "Should get user roles");
        }

        private async Task<HubActionTester<GetUserRolesRequest, AppRoleModel[]>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.AppUser.GetUserRoles);
        }
    }
}
