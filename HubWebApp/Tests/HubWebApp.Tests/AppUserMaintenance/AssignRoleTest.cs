namespace HubWebApp.Tests;

internal sealed class AssignRoleTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        var userToEdit = await AddUser(tester, "userToEdit");
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await GetRole(tester, app.ToModel(), HubInfo.Roles.ViewApp);
        var model = CreateModel(userToEdit, defaultModifier.ToModel(), viewAppRole);
        await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(model);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        var userToEdit = await AddUser(tester, "userToEdit");
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await GetRole(tester, app.ToModel(), HubInfo.Roles.ViewApp);
        var model = CreateModel(userToEdit, defaultModifier.ToModel(), viewAppRole);
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                model,
                [HubInfo.Roles.ViewApp],
                generalUserGroupModifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldAssignRoleToUser()
    {
        var tester = await Setup();
        var userToEdit = await AddUser(tester, "userToEdit");
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        await tester.Login([HubInfo.Roles.ViewApp], generalUserGroupModifier, HubInfo.Roles.EditUser);
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await GetRole(tester, app.ToModel(), HubInfo.Roles.ViewApp);
        var model = CreateModel(userToEdit, defaultModifier.ToModel(), viewAppRole);
        var hubAppModifier = await tester.HubAppModifier();
        await tester.Execute(model, generalUserGroupModifier);
        var userRoles = await GetAssignedRoles(tester, hubAppModifier, userToEdit);
        Assert.That
        (
            userRoles.Select(r => r.Name),
            Has.One.EqualTo(HubInfo.Roles.ViewApp),
            "Should assign role to user"
        );
    }

    [Test]
    public async Task ShouldReturnUserRoleID()
    {
        var tester = await Setup();
        await tester.Login(HubInfo.Roles.EditUser);
        var userToEdit = await AddUser(tester, "userToEdit");
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await GetRole(tester, app.ToModel(), HubInfo.Roles.ViewApp);
        var model = CreateModel(userToEdit, defaultModifier.ToModel(), viewAppRole);
        var hubAppModifier = await tester.HubAppModifier();
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        var userRoleID = await tester.Execute(model, generalUserGroupModifier);
        var userRoles = await GetAssignedRoles(tester, hubAppModifier, userToEdit);
        Assert.That
        (
            userRoles.Select(r => r.ID),
            Has.One.EqualTo(userRoleID),
            "Should return user role ID"
        );
    }

    private async Task<HubActionTester<UserRoleRequest, int>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUserMaintenance.AssignRole);
    }

    private UserRoleRequest CreateModel(AppUserModel userToEdit, ModifierModel modifier, AppRoleModel role)
    {
        return new UserRoleRequest
        {
            UserID = userToEdit.ID,
            ModifierID = modifier.ID,
            RoleID = role.ID
        };
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

    private async Task<AppRoleModel[]> GetAssignedRoles(IHubActionTester tester, ModifierModel userModifier, AppUserModel user)
    {
        var getRolesTester = tester.Create(hubApi => hubApi.AppUserInquiry.GetAssignedRoles);
        await getRolesTester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var assignedRoles = await getRolesTester.Execute
        (
            new UserModifierKey
            (
                modifierID: userModifier.ID,
                userID: user.ID
            ),
            modifier
        );
        return assignedRoles;
    }
}