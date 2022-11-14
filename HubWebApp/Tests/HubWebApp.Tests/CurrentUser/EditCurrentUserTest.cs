namespace HubWebApp.Tests;

internal sealed class EditCurrentUserTest
{
    [Test]
    public async Task ShouldUpdateName()
    {
        var tester = await Setup();
        var loggedInUser = await tester.Login();
        var form = CreateEditUserForm();
        form.PersonName.SetValue("Changed Name");
        await tester.Execute(form);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var userModel = (await factory.Users.User(loggedInUser.ToModel().ID)).ToModel();
        Assert.That(userModel.Name, Is.EqualTo("Changed Name"), "Should update name");
    }

    [Test]
    public async Task ShouldUpdateNameFromUserName_WhenNameIsBlank()
    {
        var tester = await Setup();
        var loggedInUser = await tester.Login();
        var form = CreateEditUserForm();
        form.PersonName.SetValue("");
        var modifier = await tester.GeneralUserGroupModifier();
        await tester.Execute( form, modifier);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var userModel = (await factory.Users.User(loggedInUser.ToModel().ID)).ToModel();
        Assert.That(userModel.Name, Is.EqualTo(loggedInUser.ToModel().UserName.Value), "Should update name from user name when name is blank");
    }

    [Test]
    public async Task ShouldUpdateEmail()
    {
        var tester = await Setup();
        var loggedInUser = await tester.Login(HubInfo.Roles.EditUser);
        var form = CreateEditUserForm();
        form.Email.SetValue("changed@gmail.com");
        var modifier = await tester.GeneralUserGroupModifier();
        await tester.Execute(form, modifier);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var userModel = (await factory.Users.User(loggedInUser.ToModel().ID)).ToModel();
        Assert.That(userModel.Email, Is.EqualTo("changed@gmail.com"), "Should update email");
    }

    private static EditCurrentUserForm CreateEditUserForm()=> new EditCurrentUserForm();

    private async Task<HubActionTester<EditCurrentUserForm, AppUserModel>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.CurrentUser.EditUser);
    }
}