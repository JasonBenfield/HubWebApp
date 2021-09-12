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
            var addUserRole = await hubApp.Role(HubInfo.Roles.AddUser);
            await adminUser.AddRole(addUserRole);
            var defaultModifier = await hubApp.DefaultModifier();
            var request = new GetUserRolesRequest
            {
                UserID = adminUser.ID.Value,
                ModifierID = defaultModifier.ID.Value
            };
            var roles = await tester.Execute(request, adminUser);
            Assert.That(roles.Select(r => r.Name), Has.One.EqualTo(HubInfo.Roles.AddUser.Value), "Should get user roles");
        }

        private async Task<HubActionTester<GetUserRolesRequest, AppRoleModel[]>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.AppUser.GetUserRoles);
        }
    }
}
