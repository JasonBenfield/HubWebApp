namespace HubWebApp.Tests;

internal sealed class GetUnassignedRolesTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        var modifier = await tester.GeneralUserGroupModifier();
        var user = await AddUser(tester, "someone");
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
        var hubAppModifier = await tester.HubAppModifier();
        var user = await AddUser(tester, "someone");
        var request = new UserModifierKey
        (
            userID: user.ID,
            modifierID: hubAppModifier.ID
        );
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                [HubInfo.Roles.ViewApp],
                generalUserGroupModifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.ViewUser,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldGetUnassignedRoles()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var hubApp = await tester.HubApp();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await AddUser(tester, "someone");
        var viewUserRole = await GetRole(tester, hubApp.ToModel(), HubInfo.Roles.ViewUser);
        await AssignRole(tester, user, hubAppModifier, viewUserRole);
        var request = new UserModifierKey
        (
            userID: user.ID,
            modifierID: hubAppModifier.ID
        );
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        var unassignedRoles = await tester.Execute(request, generalUserGroupModifier);
        var unassignedRoleNames = unassignedRoles.Select(r => r.Name);
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
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var hubApp = await tester.HubApp();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await AddUser(tester, "someone");
        var viewUserRole = await GetRole(tester, hubApp.ToModel(), HubInfo.Roles.ViewUser);
        await AssignRole(tester, user, hubAppModifier, viewUserRole);
        var request = new UserModifierKey
        (
            userID: user.ID,
            modifierID: hubAppModifier.ID
        );
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        var unassignedRoles = await tester.Execute(request, generalUserGroupModifier);
        Assert.That
        (
            unassignedRoles.Select(r => r.Name),
            Has.None.EqualTo(AppRoleName.DenyAccess)
        );
    }

    private async Task<HubActionTester<UserModifierKey, AppRoleModel[]>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUser.GetExplicitlyUnassignedRoles);
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

    private async Task<int> AssignRole(IHubActionTester tester, AppUserModel user, ModifierModel modifier, AppRoleModel role)
    {
        var assignRoleTester = tester.Create(hubApi => hubApi.AppUserMaintenance.AssignRole);
        await assignRoleTester.LoginAsAdmin();
        var app = await tester.HubApp();
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        var userRoleID = await assignRoleTester.Execute
        (
            new UserRoleRequest
            {
                UserID = user.ID,
                ModifierID = modifier.ID,
                RoleID = role.ID
            },
            generalUserGroupModifier
        );
        return userRoleID;
    }

}