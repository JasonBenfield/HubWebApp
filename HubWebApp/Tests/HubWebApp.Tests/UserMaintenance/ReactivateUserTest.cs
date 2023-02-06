namespace HubWebApp.Tests;

internal sealed class ReactivateUserTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        var userToReactivate = await AddUser(tester, "userToReactivate");
        var userID = userToReactivate.ToModel().ID;
        await DeactivateUser(tester, userID);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(userID);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        var modifier = await tester.GeneralUserGroupModifier();
        var userToReactivate = await AddUser(tester, "userToReactivate");
        var userID = userToReactivate.ToModel().ID;
        await DeactivateUser(tester, userID);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                userID,
                modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldReactivateUser()
    {
        var tester = await Setup();
        await tester.Login(HubInfo.Roles.EditUser);
        var userToReactivate = await AddUser(tester, "userToReactivate");
        var userID = userToReactivate.ToModel().ID;
        await DeactivateUser(tester, userID);
        await tester.Execute(userID, new ModifierKey("General"));
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var userModel = (await factory.Users.User(userID)).ToModel();
        Assert.That(userModel.IsActive(), Is.True, "Should reactivate user");
    }

    private async Task<HubActionTester<int, AppUserModel>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.UserMaintenance.ReactivateUser);
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

    private Task<AppUserModel> DeactivateUser(IHubActionTester tester, int userID)=>
        tester.Create(api => api.UserMaintenance.DeactivateUser).Execute(userID, new ModifierKey("General"));
}