using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_HubAppApi;
using XTI_HubAppApi.UserList;

namespace HubWebApp.Tests
{
    public static class AccessAssertions
    {
        public static AppModifierAssertions<TModel, TResult> Create<TModel, TResult>(HubActionTester<TModel, TResult> tester)
        {
            return new AppModifierAssertions<TModel, TResult>(tester);
        }
    }

    public sealed class AppModifierAssertions<TModel, TResult>
    {
        private readonly HubActionTester<TModel, TResult> tester;

        public AppModifierAssertions(HubActionTester<TModel, TResult> tester)
        {
            this.tester = tester;
        }

        public async Task ShouldThrowError_WhenModifierIsBlank(TModel model)
        {
            var adminUser = await tester.AdminUser();
            var ex = Assert.ThrowsAsync<Exception>(() => tester.Execute(model, adminUser));
            Assert.That(ex.Message, Is.EqualTo(AppErrors.ModifierIsRequired));
        }

        public async Task ShouldThrowError_WhenModifierIsNotFound(TModel model)
        {
            var adminUser = await tester.AdminUser();
            var modKey = new ModifierKey("NotFound");
            Assert.ThrowsAsync<AccessDeniedException>(() => tester.Execute(model, adminUser, modKey));
        }

        public async Task ShouldThrowError_WhenModifierIsNotAssignedToUser(TModel model, Modifier modifier)
        {
            var loggedInUser = await addUser(tester, "assertions_user");
            Assert.ThrowsAsync<AccessDeniedException>(() => tester.Execute(model, loggedInUser, modifier.ModKey()));
        }

        public async Task ShouldThrowError_WhenModifierIsNotAssignedToUser_ButRoleIsAssignedToUser(TModel model, AppRole role, Modifier modifier)
        {
            var loggedInUser = await addUser(tester, "assertions_user");
            await loggedInUser.AddRole(role);
            var denyAccessRole = await tester.DenyAccessRole();
            await loggedInUser.AddRole(denyAccessRole, modifier);
            Assert.ThrowsAsync<AccessDeniedException>(() => tester.Execute(model, loggedInUser, modifier.ModKey()));
        }

        public async Task ShouldThrowError_WhenRoleIsNotAssignedToUser(TModel model)
        {
            var loggedInUser = await addUser(tester, "assertions_user");
            Assert.ThrowsAsync<AccessDeniedException>(() => tester.Execute(model, loggedInUser));
        }

        public async Task ShouldThrowError_WhenModifiedRoleIsNotAssignedToUser(TModel model, Modifier modifier)
        {
            var loggedInUser = await addUser(tester, "assertions_user");
            var denyAccessRole = await tester.DenyAccessRole();
            await loggedInUser.AddRole(denyAccessRole, modifier);
            Assert.ThrowsAsync<AccessDeniedException>(() => tester.Execute(model, loggedInUser, modifier.ModKey()));
        }

        private async Task<AppUser> addUser(HubActionTester<TModel, TResult> tester, string userName)
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
