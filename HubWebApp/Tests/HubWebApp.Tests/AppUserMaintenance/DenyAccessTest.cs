using XTI_HubWebAppApi.UserList;

namespace HubWebApp.Tests;

internal sealed class DenyAccessTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var user = await addUser(tester, "someone");
        var modifier = await tester.HubAppModifier();
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = modifier.ID
        };
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(request);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var user = await addUser(tester, "someone");
        var modifier = await tester.HubAppModifier();
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = modifier.ID
        };
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                tester.FakeHubAppModifier(),
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldAddDenyAccessRole()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var user = await addUser(tester, "someone");
        var modifier = await tester.HubAppModifier();
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = modifier.ID
        };
        await tester.Execute(request, modifier.ModKey());
        var roles = await user.Modifier(modifier).AssignedRoles();
        Assert.That
        (
            roles.Select(r => r.Name()),
            Is.EqualTo(new[] { AppRoleName.DenyAccess }),
            "Should add deny access role"
        );
    }

    [Test]
    public async Task ShouldRemoveOtherRoles()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var adminRole = await tester.AdminRole();
        var user = await addUser(tester, "someone");
        var modifier = await tester.HubAppModifier();
        await user.Modifier(modifier).AssignRole(adminRole);
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = modifier.ID
        };
        await tester.Execute(request, modifier.ModKey());
        var roles = await user.Modifier(modifier).AssignedRoles();
        Assert.That
        (
            roles.Select(r => r.Name()),
            Is.EqualTo(new[] { AppRoleName.DenyAccess }),
            "Should remove existing roles"
        );
    }

    private async Task<HubActionTester<UserModifierKey, EmptyActionResult>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUserMaintenance.DenyAccess);
    }

    private async Task<AppUser> addUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        addUserTester.LoginAsAdmin();
        var userID = await addUserTester.Execute(new AddUserModel
        {
            UserName = userName,
            Password = "Password12345"
        });
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var user = await factory.Users.UserByUserName(new AppUserName(userName));
        return user;
    }
}