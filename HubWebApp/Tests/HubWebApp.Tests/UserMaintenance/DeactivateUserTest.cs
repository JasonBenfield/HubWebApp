namespace HubWebApp.Tests;

internal sealed class DeactivateUserTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        var userToDeactivate = await AddUser(tester, "userToDeactivate");
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(userToDeactivate.ToModel().ID);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        var modifier = await tester.GeneralUserGroupModifier();
        var userToDeactivate = await AddUser(tester, "userToDeactivate");
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                userToDeactivate.ToModel().ID,
                modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldDeactivateUser()
    {
        var tester = await Setup();
        await tester.Login(HubInfo.Roles.EditUser);
        var userToDeactivate = await AddUser(tester, "userToDeactivate");
        await tester.Execute(userToDeactivate.ToModel().ID, new ModifierKey("General"));
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var userModel = (await factory.Users.User(userToDeactivate.ToModel().ID)).ToModel();
        Assert.That(userModel.IsActive(), Is.False, "Should deactivate user");
    }

    private async Task<HubActionTester<int, AppUserModel>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.UserMaintenance.DeactivateUser);
    }

    private async Task<AppUser> AddUser(IHubActionTester tester, string userName)
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
}