using XTI_HubWebAppApi.AppUserInquiry;
using XTI_HubWebAppApi.UserList;

namespace HubWebApp.Tests;

internal sealed class GetUserAccessTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
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
        tester.LoginAsAdmin();
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
    public async Task ShouldGetUserRolesForModifier()
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
        var userAccess = await tester.Execute(request, hubAppModifier.ModKey());
        Assert.That
        (
            userAccess.AssignedRoles.Select(r => new AppRoleName(r.Name)),
            Has.One.EqualTo(HubInfo.Roles.ViewUser),
            "Should get user roles for modifier"
        );
    }

    [Test]
    public async Task ShouldReturnAccessDenied_WhenAccessIsDenied()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await addUser(tester, "someone");
        await denyAccess(tester, user, hubAppModifier);
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = hubAppModifier.ID
        };
        var userAccess = await tester.Execute(request, hubAppModifier.ModKey());
        Assert.That
        (
            userAccess.HasAccess,
            Is.False,
            "Should not have access"
        );
    }

    [Test]
    public async Task ShouldNotReturnDenyAccessRole()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await addUser(tester, "someone");
        await denyAccess(tester, user, hubAppModifier);
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = hubAppModifier.ID
        };
        var userAccess = await tester.Execute(request, hubAppModifier.ModKey());
        Assert.That
        (
            userAccess.AssignedRoles.Any(r => AppRoleName.DenyAccess.Equals(r.Name)),
            Is.False,
            "Should not include deny access role"
        );
    }

    [Test]
    public async Task ShouldOnlyGetExplicitlyAssignedRoles()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await addUser(tester, "someone");
        var hubApp = await tester.HubApp();
        var defaultModifier = await hubApp.DefaultModifier();
        var role = await hubApp.Role(HubInfo.Roles.EditUser);
        await user.Modifier(defaultModifier).AssignRole(role);
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = hubAppModifier.ID
        };
        var userAccess = await tester.Execute(request, hubAppModifier.ModKey());
        Assert.That
        (
            userAccess.AssignedRoles.Select(r => new AppRoleName(r.Name)),
            Has.None.EqualTo(HubInfo.Roles.EditUser),
            "Should only include explicitly assigned roles"
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

    private async Task denyAccess(IHubActionTester tester, AppUser user, Modifier modifier)
    {
        var denyAccessTester = tester.Create(hubApi => hubApi.AppUserMaintenance.DenyAccess);
        denyAccessTester.LoginAsAdmin();
        var hubAppModifier = denyAccessTester.FakeHubAppModifier();
        await denyAccessTester.Execute
        (
            new UserModifierKey
            {
                UserID = user.ID,
                ModifierID = modifier.ID
            },
            hubAppModifier.ModKey()
        );
    }

    private async Task<HubActionTester<UserModifierKey, UserAccessModel>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUser.GetUserAccess);
    }
}