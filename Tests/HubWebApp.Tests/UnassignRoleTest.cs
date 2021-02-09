using Microsoft.Extensions.DependencyInjection;
using HubWebAppApi.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_App;
using HubWebAppApi;
using NUnit.Framework;
using XTI_App.Api;

namespace HubWebApp.Tests
{
    public sealed class UnassignRoleTest
    {
        [Test]
        public async Task ShouldThrowError_WhenModifierIsBlank()
        {
            var tester = await setup();
            var userToEdit = await addUser(tester, "userToEdit");
            var viewAppRole = await getViewAppRole(tester);
            var userRoleID = await assignRole(tester, userToEdit, viewAppRole);
            await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(userRoleID);
        }

        [Test]
        public async Task ShouldThrowError_WhenUserIsNotAssignedToAnAllowedRole()
        {
            var tester = await setup();
            var userToEdit = await addUser(tester, "userToEdit");
            var hubAppModifier = await tester.HubAppModifier();
            await userToEdit.AddModifier(hubAppModifier);
            var viewAppRole = await getViewAppRole(tester);
            var userRoleID = await assignRole(tester, userToEdit, viewAppRole);
            await AccessAssertions.Create(tester)
                .ShouldThrowError_WhenRoleIsNotAssignedToUserButModifierIsAssignedToUser
                (
                    userRoleID,
                    hubAppModifier
                );
        }

        [Test]
        public async Task ShouldUnassignRoleFromUser()
        {
            var tester = await setup();
            var loggedInUser = await addUser(tester, "loggedinUser");
            await grantUserAccess(tester, loggedInUser);
            var userToEdit = await addUser(tester, "userToEdit");
            var app = await tester.HubApp();
            var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
            var model = createModel(userToEdit, viewAppRole);
            var userRoleID = await assignRole(tester, userToEdit, viewAppRole);
            var hubAppModifier = await tester.HubAppModifier();
            await tester.Execute(userRoleID, loggedInUser, hubAppModifier.ModKey());
            var userRoles = await userToEdit.Roles(app);
            Assert.That
            (
                userRoles.Select(r => r.Name()),
                Has.None.EqualTo(HubInfo.Roles.ViewApp),
                "Should unassign role from user"
            );
        }

        private async Task<HubActionTester<int, EmptyActionResult>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.AppUserMaintenance.UnassignRole);
        }

        private static async Task<AppRole> getViewAppRole(IHubActionTester tester)
        {
            var app = await tester.HubApp();
            var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
            return viewAppRole;
        }

        private AssignRoleRequest createModel(AppUser userToEdit, AppRole role)
        {
            return new AssignRoleRequest
            {
                UserID = userToEdit.ID.Value,
                RoleID = role.ID.Value
            };
        }

        private async Task<AppUser> addUser(IHubActionTester tester, string userName)
        {
            var addUserTester = tester.Create(hubApi => hubApi.Users.AddUser);
            var adminUser = await addUserTester.AdminUser();
            var userID = await addUserTester.Execute(new AddUserModel
            {
                UserName = userName,
                Password = "Password12345"
            }, adminUser);
            var factory = tester.Services.GetService<AppFactory>();
            var user = await factory.Users().User(new AppUserName(userName));
            return user;
        }

        private async Task<int> assignRole(IHubActionTester tester, AppUser user, AppRole role)
        {
            var assignRoleTester = tester.Create(hubApi => hubApi.AppUserMaintenance.AssignRole);
            var adminUser = await assignRoleTester.AdminUser();
            var hubAppModifier = await tester.HubAppModifier();
            await adminUser.AddModifier(hubAppModifier);
            var userRoleID = await assignRoleTester.Execute(new AssignRoleRequest
            {
                UserID = user.ID.Value,
                RoleID = role.ID.Value
            }, adminUser, hubAppModifier.ModKey());
            return userRoleID;
        }

        private async Task grantUserAccess(IHubActionTester tester, AppUser user)
        {
            var app = await tester.HubApp();
            var editUserRole = await app.Role(HubInfo.Roles.EditUser);
            await user.AddRole(editUserRole);
            var hubAppModifier = await tester.HubAppModifier();
            await user.AddModifier(hubAppModifier);
        }

    }
}
