using XTI_HubWebAppApi.UserList;

namespace HubWebApp.Tests;

internal sealed class AllowAccessTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var user = await addUser(tester, "someone");
        var modifier = await tester.HubAppModifier();
        var request = new UserModifierKey
        {
            UserID = user.ToModel().ID,
            ModifierID = modifier.ID
        };
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(request);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var user = await addUser(tester, "someone");
        var modifier = await tester.HubAppModifier();
        var request = new UserModifierKey
        {
            UserID = user.ToModel().ID,
            ModifierID = modifier.ID
        };
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldRemoveDenyAccessRole()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var user = await addUser(tester, "someone");
        var hubApp = await tester.HubApp();
        var denyAccessRole = await hubApp.Role(AppRoleName.DenyAccess);
        var modifier = await tester.HubAppModifier();
        await user.Modifier(modifier).AssignRole(denyAccessRole);
        var request = new UserModifierKey
        {
            UserID = user.ToModel().ID,
            ModifierID = modifier.ID
        };
        await tester.Execute(request, modifier.ModKey());
        var roles = await user.Modifier(modifier).AssignedRoles();
        Assert.That
        (
            roles.Select(r => r.ToModel().Name),
            Is.EqualTo(new AppRoleName[0]),
            "Should remove deny access role"
        );
    }

    private async Task<HubActionTester<UserModifierKey, EmptyActionResult>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUserMaintenance.AllowAccess);
    }

    private async Task<AppUser> addUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        await addUserTester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var userID = await addUserTester.Execute
        (
            new AddUserModel
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