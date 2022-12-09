namespace HubWebApp.Tests;

internal sealed class GetUserOrAnonTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        await AddUser(tester, "User1");
        await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank("User1");
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        await AddUser(tester, "User1");
        var modifier = await tester.GeneralUserGroupModifier();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                "User1",
                modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.ViewUser
            );
    }

    [Test]
    public async Task ShouldGetUserByUserName()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var addedUser = await AddUser(tester, "User1");
        var modifier = await tester.GeneralUserGroupModifier();
        var user = await tester.Execute("User1", modifier);
        Assert.That(user, Is.EqualTo(addedUser), "Should get user by user name");
    }

    [Test]
    public async Task ShouldGetAnonUser_WhenNotFound()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var user = await tester.Execute("User1", modifier);
        Assert.That(user.UserName, Is.EqualTo(AppUserName.Anon), "Should get anon user when not found");
    }

    private async Task<HubActionTester<string, AppUserModel>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        return HubActionTester.Create(sp, hubApi => hubApi.UserInquiry.GetUserOrAnon);
    }

    private async Task<AppUserModel> AddUser(IHubActionTester tester, string userName)
    {
        var form = new AddUserForm();
        form.UserName.SetValue(userName);
        form.Password.SetValue("Password1234");
        form.Confirm.SetValue("Password1234");
        form.PersonName.SetValue(userName);
        form.Email.SetValue($"{userName}@example.com");
        var addTester = tester.Create(api => api.Users.AddUser);
        var modifier = await tester.GeneralUserGroupModifier();
        var user = await addTester.Execute(form, modifier);
        return user;
    }
}
