namespace HubWebApp.Tests;

internal sealed class DenyAccessTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var user = await AddUser(tester, "someone");
        var modifier = await tester.HubAppModifier();
        var request = new UserModifierKey
        (
            userID: user.ID,
            modifierID: modifier.ID
        );
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(request);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var user = await AddUser(tester, "someone");
        var modifier = await tester.HubAppModifier();
        var request = new UserModifierKey
        (
            userID: user.ID,
            modifierID: modifier.ID
        );
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                [HubInfo.Roles.ViewApp],
                generalUserGroupModifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldAddDenyAccessRole()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var user = await AddUser(tester, "someone");
        var modifier = await tester.HubAppModifier();
        var request = new UserModifierKey
        (
            userID: user.ID,
            modifierID: modifier.ID
        );
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        await tester.Execute(request, generalUserGroupModifier);
        var userAccess = await GetExplicitUserAccess(tester, user, modifier);
        Assert.That
        (
            userAccess.HasAccess,
            Is.False,
            "Should deny access"
        );
    }

    [Test]
    public async Task ShouldRemoveOtherRoles()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var hubApp = await tester.HubApp();
        var adminRole = await GetRole(tester, hubApp.ToModel(), HubInfo.Roles.Admin);
        var user = await AddUser(tester, "someone");
        var modifier = await tester.HubAppModifier();
        await AssignRole(tester, user, adminRole);
        var request = new UserModifierKey
        (
            userID: user.ID,
            modifierID: modifier.ID
        );
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        await tester.Execute(request, generalUserGroupModifier);
        var userAccess = await GetExplicitUserAccess(tester, user, modifier);
        Assert.That
        (
            userAccess.AssignedRoles.Select(r => r.Name),
            Is.EqualTo(new AppRoleName[0]),
            "Should remove existing roles"
        );
    }

    private async Task<HubActionTester<UserModifierKey, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUserMaintenance.DenyAccess);
    }

    private async Task<AppUserModel> AddUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        await addUserTester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var user = await addUserTester.Execute
        (
            new AddOrUpdateUserRequest
            {
                UserName = userName,
                Password = "Password12345"
            },
            modifier
        );
        return user;
    }

    private static async Task<AppRoleModel> GetRole(IHubActionTester sourceTester, AppModel app, AppRoleName roleName)
    {
        var tester = sourceTester.Create(api => api.App.GetRoles);
        var roles = await tester.Execute(new EmptyRequest(), app.PublicKey);
        return roles.FirstOrDefault(r => r.Name.Equals(roleName)) ?? new();
    }

    private async Task<int> AssignRole(IHubActionTester tester, AppUserModel user, AppRoleModel role)
    {
        var assignRoleTester = tester.Create(hubApi => hubApi.AppUserMaintenance.AssignRole);
        await assignRoleTester.LoginAsAdmin();
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        var userRoleID = await assignRoleTester.Execute
        (
            new UserRoleRequest
            {
                UserID = user.ID,
                ModifierID = defaultModifier.ID,
                RoleID = role.ID
            },
            generalUserGroupModifier
        );
        return userRoleID;
    }

    private async Task<UserAccessModel> GetExplicitUserAccess(IHubActionTester sourceTester, AppUserModel user, ModifierModel modifier)
    {
        var tester = sourceTester.Create(hubApi => hubApi.AppUserInquiry.GetExplicitUserAccess);
        await tester.LoginAsAdmin();
        var app = await sourceTester.HubApp();
        var generalUserGroupModifier = await sourceTester.GeneralUserGroupModifier();
        var userAccess = await tester.Execute
        (
            new UserModifierKey
            (
                modifierID: modifier.ID,
                userID: user.ID
            ),
            generalUserGroupModifier
        );
        return userAccess;
    }
}