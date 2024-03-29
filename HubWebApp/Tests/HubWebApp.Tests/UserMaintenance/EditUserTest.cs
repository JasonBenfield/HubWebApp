﻿namespace HubWebApp.Tests;

internal sealed class EditUserTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        var userToEdit = await addUser(tester, "userToEdit");
        var form = createEditUserForm(userToEdit);
        await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(form);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        var modifier = await tester.GeneralUserGroupModifier();
        var userToEdit = await addUser(tester, "userToEdit");
        var form = createEditUserForm(userToEdit);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                form,
                modifier,
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
        var modifier = await tester.GeneralUserGroupModifier();
        await tester.Execute(form, modifier);
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
        var modifier = await tester.GeneralUserGroupModifier();
        await tester.Execute( form, modifier);
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
        var modifier = await tester.GeneralUserGroupModifier();
        await tester.Execute(form, modifier);
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
        var modifier = await tester.GeneralUserGroupModifier();
        var userID = await addUserTester.Execute
        (
            new AddOrUpdateUserRequest
            {
                UserName = userName,
                Password = "Password12345"
            },
            modifier
        );
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