using HubWebAppApi;
using HubWebAppApi.Users;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;

namespace HubWebApp.Tests
{
    public sealed class GetUserModCategoriesTest
    {
        [Test]
        public async Task ShouldThrowError_WhenModifierIsBlank()
        {
            var tester = await setup();
            var user = await addUser(tester, "some.user");
            await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(user.ID.Value);
        }

        [Test]
        public async Task ShouldThrowError_WhenModifierIsNotFound()
        {
            var tester = await setup();
            var user = await addUser(tester, "some.user");
            await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsNotFound(user.ID.Value);
        }

        [Test]
        public async Task ShouldThrowError_WhenUserIsNotAssignedToAnAllowedRole()
        {
            var tester = await setup();
            var user = await addUser(tester, "some.user");
            var modifier = await tester.HubAppModifier();
            await AccessAssertions.Create(tester)
                .ShouldThrowError_WhenRoleIsNotAssignedToUserButModifierIsAssignedToUser
                (
                    user.ID.Value,
                    modifier
                );
        }

        [Test]
        public async Task ShouldGetUserModCategories_UserDoesNotHaveAccessTo()
        {
            var tester = await setup();
            var loggedInUser = await addUser(tester, "loggedinUser");
            await grantUserAccess(tester, loggedInUser);
            var user = await addUser(tester, "some.user");
            var hubAppModifier = await tester.HubAppModifier();
            var modCategories = await tester.Execute(user.ID.Value, loggedInUser, hubAppModifier.ModKey());
            Assert.That(modCategories.Length, Is.EqualTo(1), "Should get one modifier category");
            Assert.That(modCategories[0].ModCategory.Name, Is.EqualTo(HubInfo.ModCategories.Apps.DisplayText));
            Assert.That(modCategories[0].HasAccessToAll, Is.False, "Should not have access to all modifiers in the modifier category");
            Assert.That(modCategories[0].Modifiers.Length, Is.EqualTo(0), "Should noot have access to any modifiers in the modifier category");
        }

        [Test]
        public async Task ShouldGetUserModCategories_WhenUserHasAccessToAllModifiers()
        {
            var tester = await setup();
            var loggedInUser = await addUser(tester, "loggedinUser");
            await grantUserAccess(tester, loggedInUser);
            var user = await addUser(tester, "some.user");
            var hubAppModifier = await tester.HubAppModifier();
            var app = await tester.HubApp();
            var appsModCategory = await app.ModCategory(HubInfo.ModCategories.Apps);
            await user.GrantFullAccessToModCategory(appsModCategory);
            var modCategories = await tester.Execute(user.ID.Value, loggedInUser, hubAppModifier.ModKey());
            Assert.That(modCategories[0].HasAccessToAll, Is.True, "Should have access to all modifiers in the modifier category");
            Assert.That(modCategories[0].Modifiers.Length, Is.EqualTo(0), "Should not get modifiers if the user has access to all");
        }

        [Test]
        public async Task ShouldGetUserModifiers_WhenUserDoesNotHaveAccessToAllModifiers()
        {
            var tester = await setup();
            var loggedInUser = await addUser(tester, "loggedinUser");
            await grantUserAccess(tester, loggedInUser);
            var user = await addUser(tester, "some.user");
            var hubAppModifier = await tester.HubAppModifier();
            await user.AddModifier(hubAppModifier);
            var modCategories = await tester.Execute(user.ID.Value, loggedInUser, hubAppModifier.ModKey());
            Assert.That(modCategories[0].HasAccessToAll, Is.False, "Should not have access to all modifiers in the modifier category");
            Assert.That(modCategories[0].Modifiers.Length, Is.EqualTo(1), "Should have access to one modifier");
            Assert.That(modCategories[0].Modifiers[0].ModKey, Is.EqualTo(hubAppModifier.ModKey()), "Should have access to one modifier");
        }

        private async Task<HubActionTester<int, UserModifierCategoryModel[]>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.AppUser.GetUserModCategories);
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

        private async Task grantUserAccess(IHubActionTester tester, AppUser user)
        {
            var app = await tester.HubApp();
            var viewUserRole = await app.Role(HubInfo.Roles.ViewUser);
            await user.AddRole(viewUserRole);
            var hubAppModifier = await tester.HubAppModifier();
            await user.AddModifier(hubAppModifier);
        }

    }
}
