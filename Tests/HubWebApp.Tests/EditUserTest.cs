using XTI_HubAppApi;
using XTI_HubAppApi.Users;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_HubAppApi.UserList;

namespace HubWebApp.Tests
{
    public sealed class EditUserTest
    {
        [Test]
        public async Task ShouldThrowError_WhenRoleIsNotAssignedToUser()
        {
            var tester = await setup();
            var userToEdit = await addUser(tester, "userToEdit");
            var form = createEditUserForm(userToEdit);
            await AccessAssertions.Create(tester).ShouldThrowError_WhenRoleIsNotAssignedToUser(form);
        }

        [Test]
        public async Task ShouldUpdateName()
        {
            var tester = await setup();
            var loggedInUser = await addUser(tester, "loggedinUser");
            await addEditUserRole(tester, loggedInUser);
            var userToEdit = await addUser(tester, "userToEdit");
            var form = createEditUserForm(userToEdit);
            form.PersonName.SetValue("Changed Name");
            await tester.Execute(form, loggedInUser);
            var factory = tester.Services.GetService<AppFactory>();
            var userModel = (await factory.Users().User(userToEdit.ID.Value)).ToModel();
            Assert.That(userModel.Name, Is.EqualTo("Changed Name"), "Should update name");
        }

        [Test]
        public async Task ShouldUpdateNameFromUserName_WhenNameIsBlank()
        {
            var tester = await setup();
            var loggedInUser = await addUser(tester, "loggedinUser");
            await addEditUserRole(tester, loggedInUser);
            var userToEdit = await addUser(tester, "userToEdit");
            var form = createEditUserForm(userToEdit);
            form.PersonName.SetValue("");
            await tester.Execute(form, loggedInUser);
            var factory = tester.Services.GetService<AppFactory>();
            var userModel = (await factory.Users().User(userToEdit.ID.Value)).ToModel();
            Assert.That(userModel.Name, Is.EqualTo("usertoedit"), "Should update name from user name when name is blank");
        }

        [Test]
        public async Task ShouldUpdateEmail()
        {
            var tester = await setup();
            var loggedInUser = await addUser(tester, "loggedinUser");
            await addEditUserRole(tester, loggedInUser);
            var userToEdit = await addUser(tester, "userToEdit");
            var form = createEditUserForm(userToEdit);
            form.Email.SetValue("changed@gmail.com");
            await tester.Execute(form, loggedInUser);
            var factory = tester.Services.GetService<AppFactory>();
            var userModel = (await factory.Users().User(userToEdit.ID.Value)).ToModel();
            Assert.That(userModel.Email, Is.EqualTo("changed@gmail.com"), "Should update email");
        }

        private static EditUserForm createEditUserForm(AppUser userToEdit)
        {
            var form = new EditUserForm();
            form.UserID.SetValue(userToEdit.ID.Value);
            return form;
        }

        private static async Task addEditUserRole(HubActionTester<EditUserForm, EmptyActionResult> tester, AppUser loggedInUser)
        {
            var app = await tester.HubApp();
            var editUserRole = await app.Role(HubInfo.Roles.EditUser);
            await loggedInUser.AddRole(editUserRole);
        }

        private async Task<AppUser> addUser(HubActionTester<EditUserForm, EmptyActionResult> tester, string userName)
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

        private async Task<HubActionTester<EditUserForm, EmptyActionResult>> setup()
        {
            var host = new HubTestHost();
            var services = await host.Setup();
            return HubActionTester.Create(services, hubApi => hubApi.UserMaintenance.EditUser);
        }
    }
}
