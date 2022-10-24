namespace HubWebApp.Tests;

internal sealed class GetUserTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var addedUser = await AddUser(tester, "User1");
        await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(addedUser.ID);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var addedUser = await AddUser(tester, "User1");
        var modifier = await tester.GeneralUserGroupModifier();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                addedUser.ID,
                modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.ViewUser
            );
    }

    [Test]
    public async Task ShouldGetUserByID()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var addedUser = await AddUser(tester, "User1");
        var user = await tester.Execute(addedUser.ID, new ModifierKey("General"));
        Assert.That(user, Is.EqualTo(addedUser), "Should get user by ID");
    }

    private async Task<HubActionTester<int, AppUserModel>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        return HubActionTester.Create(sp, hubApi => hubApi.UserInquiry.GetUser);
    }

    private Task<AppUserModel> AddUser(IHubActionTester tester, string userName)
    {
        var form = new AddUserForm();
        form.UserName.SetValue(userName);
        form.Password.SetValue("Password1234");
        form.Confirm.SetValue("Password1234");
        form.PersonName.SetValue(userName);
        form.Email.SetValue($"{userName}@example.com");
        var addTester = tester.Create(api => api.Users.AddUser);
        return addTester.Execute(form, new ModifierKey("General"));
    }
}
