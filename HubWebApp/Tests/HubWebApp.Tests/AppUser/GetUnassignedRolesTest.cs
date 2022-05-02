using XTI_HubAppApi.UserList;

namespace HubWebApp.Tests;

internal sealed class GetUnassignedRolesTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await addUser(tester, "someone");
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = hubAppModifier.ID
        };
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(request);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await addUser(tester, "someone");
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = hubAppModifier.ID
        };
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                tester.FakeHubAppModifier(),
                HubInfo.Roles.Admin,
                HubInfo.Roles.ViewApp,
                HubInfo.Roles.ViewUser
            );
    }

    [Test]
    public async Task ShouldGetUnassignedRoles()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var hubApp = await tester.HubApp();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await addUser(tester, "someone");
        var viewUserRole = await hubApp.Role(HubInfo.Roles.ViewUser);
        await user.Modifier(hubAppModifier).AssignRole(viewUserRole);
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = hubAppModifier.ID
        };
        var unassignedRoles = await tester.Execute(request, hubAppModifier.ModKey());
        var unassignedRoleNames = unassignedRoles.Select(r => new AppRoleName(r.Name));
        Assert.That
        (
            unassignedRoleNames,
            Has.None.EqualTo(HubInfo.Roles.ViewUser)
        );
        Assert.That
        (
            unassignedRoleNames,
            Has.One.EqualTo(HubInfo.Roles.ViewApp)
        );
    }

    [Test]
    public async Task ShouldNotIncludeDenyAccessRole()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var hubApp = await tester.HubApp();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await addUser(tester, "someone");
        var viewUserRole = await hubApp.Role(HubInfo.Roles.ViewUser);
        await user.Modifier(hubAppModifier).AssignRole(viewUserRole);
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = hubAppModifier.ID
        };
        var unassignedRoles = await tester.Execute(request, hubAppModifier.ModKey());
        Assert.That
        (
            unassignedRoles.Select(r => new AppRoleName(r.Name)),
            Has.None.EqualTo(AppRoleName.DenyAccess)
        );
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

    private async Task<HubActionTester<UserModifierKey, AppRoleModel[]>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUser.GetUnassignedRoles);
    }
}