using XTI_HubAppApi;
using XTI_HubAppApi.Users;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;
using XTI_HubAppApi.AppUserMaintenance;
using XTI_HubAppApi.UserList;

namespace HubWebApp.Tests
{
    public sealed class AssignRoleTest
    {
        [Test]
        public async Task ShouldThrowError_WhenModifierIsBlank()
        {
            var tester = await setup();
            var userToEdit = await addUser(tester, "userToEdit");
            var viewAppRole = await getViewAppRole(tester);
            var model = createModel(userToEdit, viewAppRole);
            await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(model);
        }

        [Test]
        public async Task ShouldThrowError_WhenModifierIsNotFound()
        {
            var tester = await setup();
            var userToEdit = await addUser(tester, "userToEdit");
            var viewAppRole = await getViewAppRole(tester);
            var model = createModel(userToEdit, viewAppRole);
            await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsNotFound(model);
        }

        [Test]
        public async Task ShouldThrowError_WhenModifierIsNotAssignedToUser()
        {
            var tester = await setup();
            var userToEdit = await addUser(tester, "userToEdit");
            var viewAppRole = await getViewAppRole(tester);
            var model = createModel(userToEdit, viewAppRole);
            var modifier = await tester.HubAppModifier();
            await AccessAssertions.Create(tester)
                .ShouldThrowError_WhenModifierIsNotAssignedToUser_ButRoleIsAssignedToUser
                (
                    model,
                    viewAppRole,
                    modifier
                );
        }

        [Test]
        public async Task ShouldAssignRoleToUser()
        {
            var tester = await setup();
            var loggedInUser = await addUser(tester, "loggedinUser");
            await grantUserAccess(tester, loggedInUser);
            var userToEdit = await addUser(tester, "userToEdit");
            var app = await tester.HubApp();
            var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
            var model = createModel(userToEdit, viewAppRole);
            var hubAppModifier = await tester.HubAppModifier();
            await tester.Execute(model, loggedInUser, hubAppModifier.ModKey());
            var userRoles = await userToEdit.AssignedRoles(hubAppModifier);
            Assert.That
            (
                userRoles.Select(r => r.Name()),
                Has.One.EqualTo(HubInfo.Roles.ViewApp),
                "Should assign role to user"
            );
        }

        [Test]
        public async Task ShouldReturnUserRoleID()
        {
            var tester = await setup();
            var loggedInUser = await addUser(tester, "loggedinUser");
            await grantUserAccess(tester, loggedInUser);
            var userToEdit = await addUser(tester, "userToEdit");
            var app = await tester.HubApp();
            var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
            var model = createModel(userToEdit, viewAppRole);
            var hubAppModifier = await tester.HubAppModifier();
            var userRoleID = await tester.Execute(model, loggedInUser, hubAppModifier.ModKey());
            var userRoles = await userToEdit.AssignedRoles(hubAppModifier);
            Assert.That
            (
                userRoles.Select(r => r.ID),
                Has.One.EqualTo(userRoleID),
                "Should return user role ID"
            );
        }

        private async Task grantUserAccess(IHubActionTester tester, AppUser user)
        {
            var app = await tester.HubApp();
            var editUserRole = await app.Role(HubInfo.Roles.EditUser);
            var hubAppModifier = await tester.HubAppModifier();
            await user.AddRole(editUserRole, hubAppModifier);
        }

        private async Task<HubActionTester<UserRoleRequest, int>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.AppUserMaintenance.AssignRole);
        }

        private static async Task<AppRole> getViewAppRole(IHubActionTester tester)
        {
            var app = await tester.HubApp();
            var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
            return viewAppRole;
        }

        private UserRoleRequest createModel(AppUser userToEdit, AppRole role)
        {
            return new UserRoleRequest
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
    }
}
