using XTI_HubWebAppApi.UserList;
using XTI_HubWebAppApi.UserMaintenance;

namespace HubWebApp.Tests;

internal sealed class EditUserTest
{
    [Test]
    public async Task ShouldThrowError_WhenRoleIsNotAssignedToUser()
    {
        var tester = await setup();
        await tester.Login();
        var userToEdit = await addUser(tester, "userToEdit");
        var form = createEditUserForm(userToEdit);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                form,
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldUpdateName()
    {
        var tester = await setup();
        await tester.Login(HubInfo.Roles.EditUser);
        var userToEdit = await addUser(tester, "userToEdit");
        var form = createEditUserForm(userToEdit);
        form.PersonName.SetValue("Changed Name");
        await tester.Execute(form);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var userModel = (await factory.Users.User(userToEdit.ToModel().ID)).ToModel();
        Assert.That(userModel.Name, Is.EqualTo("Changed Name"), "Should update name");
    }

    [Test]
    public async Task ShouldUpdateNameFromUserName_WhenNameIsBlank()
    {
        var tester = await setup();
        await tester.Login(HubInfo.Roles.EditUser);
        var userToEdit = await addUser(tester, "userToEdit");
        var form = createEditUserForm(userToEdit);
        form.PersonName.SetValue("");
        await tester.Execute(form);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var userModel = (await factory.Users.User(userToEdit.ToModel().ID)).ToModel();
        Assert.That(userModel.Name, Is.EqualTo("usertoedit"), "Should update name from user name when name is blank");
    }

    [Test]
    public async Task ShouldUpdateEmail()
    {
        var tester = await setup();
        await tester.Login(HubInfo.Roles.EditUser);
        var userToEdit = await addUser(tester, "userToEdit");
        var form = createEditUserForm(userToEdit);
        form.Email.SetValue("changed@gmail.com");
        await tester.Execute(form);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var userModel = (await factory.Users.User(userToEdit.ToModel().ID)).ToModel();
        Assert.That(userModel.Email, Is.EqualTo("changed@gmail.com"), "Should update email");
    }

    private static EditUserForm createEditUserForm(AppUser userToEdit)
    {
        var form = new EditUserForm();
        form.UserID.SetValue(userToEdit.ToModel().ID);
        return form;
    }

    private async Task<AppUser> addUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        await addUserTester.LoginAsAdmin();
        var userID = await addUserTester.Execute(new AddUserModel
        {
            UserName = userName,
            Password = "Password12345"
        });
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var user = await factory.Users.UserByUserName(new AppUserName(userName));
        return user;
    }

    private async Task<HubActionTester<EditUserForm, EmptyActionResult>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.UserMaintenance.EditUser);
    }
}