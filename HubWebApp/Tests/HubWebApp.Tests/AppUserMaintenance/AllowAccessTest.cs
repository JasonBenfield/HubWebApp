namespace HubWebApp.Tests;

internal sealed class AllowAccessTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var user = await AddUser(tester, "someone");
        var modifier = await tester.HubAppModifier();
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = modifier.ID
        };
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
        {
            UserID = user.ID,
            ModifierID = modifier.ID
        };
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
    public async Task ShouldAllowAccess()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var user = await AddUser(tester, "someone");
        var hubApp = await tester.HubApp();
        var denyAccessRole = await GetRole(tester, hubApp.ToModel(), AppRoleName.DenyAccess);
        var modifier = await tester.HubAppModifier();
        await AssignRole(tester, hubApp.ToModel(), user, modifier, denyAccessRole);
        var request = new UserModifierKey
        (
            userID: user.ID,
            modifierID: modifier.ID
        );
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        await tester.Execute(request, generalUserGroupModifier);
        var userAccess = await GetUserRoles(tester, modifier, user);
        Assert.That
        (
            userAccess.HasAccess,
            Is.True,
            "Should remove deny access role"
        );
    }

    private async Task<HubActionTester<UserModifierKey, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUserMaintenance.AllowAccess);
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

    private async Task AssignRole(IHubActionTester sourceTester, AppModel app, AppUserModel user, ModifierModel modifier, AppRoleModel role)
    {
        var tester = sourceTester.Create(hubApi => hubApi.AppUserMaintenance.AssignRole);
        await tester.LoginAsAdmin();
        await tester.Execute
        (
            new UserRoleRequest
            (
                userID: user.ID,
                modifierID: modifier.ID,
                roleID: role.ID
            ),
            new ModifierKey("General")
        );
    }

    private async Task<UserAccessModel> GetUserRoles(IHubActionTester tester, ModifierModel userModifier, AppUserModel user)
    {
        var getRolesTester = tester.Create(hubApi => hubApi.AppUser.GetExplicitUserAccess);
        await getRolesTester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var userAccess = await getRolesTester.Execute
        (
            new UserModifierKey
            (
                modifierID: userModifier.ID,
                userID: user.ID
            ),
            modifier
        );
        return userAccess;
    }
}