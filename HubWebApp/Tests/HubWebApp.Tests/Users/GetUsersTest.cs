namespace HubWebApp.Tests;

internal sealed class GetUsersTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(new EmptyRequest());
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        var modifier = await tester.GeneralUserGroupModifier();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                new EmptyRequest(),
                modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.ViewUser
            );
    }

    [Test]
    public async Task ShouldGetUsers()
    {
        var tester = await setup();
        var userName = new AppUserName("Test.User");
        await addUser(tester, userName);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var userGroup = await factory.UserGroups.GetGeneral();
        var users = (await userGroup.Users()).ToArray();
        Assert.That(users.Select(u => u.ToModel().UserName), Has.One.EqualTo(userName), "Should get all users");
    }

    private async Task addUser(IHubActionTester tester, AppUserName userName)
    {
        var modifier = await tester.GeneralUserGroupModifier();
        var addUserTester = tester.Create(api => api.Users.AddOrUpdateUser);
        await addUserTester.Execute
        (
            new AddOrUpdateUserRequest
            {
                UserName = userName.Value,
                Password = "Password123456"
            },
            modifier
        );
    }

    private async Task<HubActionTester<EmptyRequest, AppUserModel[]>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Users.GetUsers);
    }
}