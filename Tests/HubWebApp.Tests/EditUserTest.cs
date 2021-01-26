using HubWebApp.Api;
using HubWebApp.Core;
using HubWebApp.Fakes;
using HubWebApp.UserApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_Core;

namespace HubWebApp.Tests
{
    public sealed class EditUserTest
    {
        [Test]
        public async Task ShouldRequireUserWithEditUserRole()
        {
            var services = await setup();
            var loggedInUser = await addUser(services, "loggedinUser");
            services.LoginAs(loggedInUser);
            var userToEdit = await addUser(services, "userToEdit");
            var form = new EditUserForm();
            var hubApi = services.GetService<HubAppApi>();
            Assert.ThrowsAsync<AccessDeniedException>
            (
                () => hubApi.UserMaintenance.EditUser.Execute(form)
            );
        }

        [Test]
        public async Task ShouldUpdateName()
        {
            var services = await setup();
            var loggedInUser = await addUser(services, "loggedinUser");
            await addEditUserRole(services, loggedInUser);
            services.LoginAs(loggedInUser);
            var userToEdit = await addUser(services, "userToEdit");
            var form = createEditUserForm(userToEdit);
            form.PersonName.SetValue("Changed Name");
            var hubApi = services.GetService<HubAppApi>();
            await hubApi.UserMaintenance.EditUser.Execute(form);
            var userModel = (await services.GetService<AppFactory>().Users().User(userToEdit.ID.Value)).ToModel();
            Assert.That(userModel.Name, Is.EqualTo("Changed Name"), "Should update name");
        }

        [Test]
        public async Task ShouldUpdateNameFromUserName_WhenNameIsBlank()
        {
            var services = await setup();
            var loggedInUser = await addUser(services, "loggedinUser");
            await addEditUserRole(services, loggedInUser);
            services.LoginAs(loggedInUser);
            var userToEdit = await addUser(services, "userToEdit");
            var form = createEditUserForm(userToEdit);
            form.PersonName.SetValue("");
            var hubApi = services.GetService<HubAppApi>();
            await hubApi.UserMaintenance.EditUser.Execute(form);
            var userModel = (await services.GetService<AppFactory>().Users().User(userToEdit.ID.Value)).ToModel();
            Assert.That(userModel.Name, Is.EqualTo("usertoedit"), "Should update name from user name when name is blank");
        }

        [Test]
        public async Task ShouldUpdateEmail()
        {
            var services = await setup();
            var loggedInUser = await addUser(services, "loggedinUser");
            await addEditUserRole(services, loggedInUser);
            services.LoginAs(loggedInUser);
            var userToEdit = await addUser(services, "userToEdit");
            var form = createEditUserForm(userToEdit);
            form.Email.SetValue("changed@gmail.com");
            var hubApi = services.GetService<HubAppApi>();
            await hubApi.UserMaintenance.EditUser.Execute(form);
            var userModel = (await services.GetService<AppFactory>().Users().User(userToEdit.ID.Value)).ToModel();
            Assert.That(userModel.Email, Is.EqualTo("changed@gmail.com"), "Should update email");
        }

        private static EditUserForm createEditUserForm(AppUser userToEdit)
        {
            var form = new EditUserForm();
            form.UserID.SetValue(userToEdit.ID.Value);
            return form;
        }

        private static async Task addEditUserRole(IServiceProvider services, AppUser loggedInUser)
        {
            var app = await services.HubApp();
            var editUserRole = await app.Role(HubInfo.Roles.EditUser);
            await loggedInUser.AddRole(editUserRole);
        }

        private Task<AppUser> addUser(IServiceProvider services, string userName)
        {
            var factory = services.GetService<AppFactory>();
            var clock = services.GetService<Clock>();
            return factory.Users().Add
            (
                new AppUserName(userName),
                new FakeHashedPassword("Password12345"),
                clock.Now()
            );
        }

        private async Task<IServiceProvider> setup()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices
                (
                    (hostContext, services) =>
                    {
                        services.AddFakesForHubWebApp(hostContext.Configuration);
                    }
                )
                .Build();
            var scope = host.Services.CreateScope();
            var sp = scope.ServiceProvider;
            await scope.ServiceProvider.Setup();
            var adminUser = await sp.AddAdminUser();
            return sp;
        }
    }
}
