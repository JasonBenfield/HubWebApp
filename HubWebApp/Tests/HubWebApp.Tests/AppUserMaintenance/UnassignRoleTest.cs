namespace HubWebApp.Tests;

internal sealed class UnassignRoleTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        var userToEdit = await AddUser(tester, "userToEdit");
        var viewAppRole = await GetViewAppRole(tester);
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        await AssignRole(tester, userToEdit, defaultModifier.ToModel(), viewAppRole);
        var request = new UserRoleRequest
        (
            userID: userToEdit.ID,
            modifierID: defaultModifier.ID,
            roleID: viewAppRole.ID
        );
        await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(request);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        var userToEdit = await AddUser(tester, "userToEdit");
        var viewAppRole = await GetViewAppRole(tester);
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        await AssignRole(tester, userToEdit, defaultModifier.ToModel(), viewAppRole);
        var request = new UserRoleRequest
        (
            userID: userToEdit.ID,
            modifierID: defaultModifier.ID,
            roleID: viewAppRole.ID
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
    public async Task ShouldUnassignRoleFromUser()
    {
        var tester = await Setup();
        await tester.Login(HubInfo.Roles.EditUser);
        var userToEdit = await AddUser(tester, "userToEdit");
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await GetViewAppRole(tester);
        var model = CreateModel(userToEdit, viewAppRole);
        await AssignRole(tester, userToEdit, defaultModifier.ToModel(), viewAppRole);
        var request = new UserRoleRequest
        (
            userID: userToEdit.ID,
            modifierID: defaultModifier.ID,
            roleID: viewAppRole.ID
        );
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        await tester.Execute(request, generalUserGroupModifier);
        var hubAppModifier = await tester.HubAppModifier();
        var userRoles = await GetExplicitlyAssignedRoles(tester, userToEdit);
        Assert.That
        (
            userRoles.Select(r => r.Name),
            Has.None.EqualTo(HubInfo.Roles.ViewApp),
            "Should unassign role from user"
        );
    }

    private async Task<HubActionTester<UserRoleRequest, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUserMaintenance.UnassignRole);
    }

    private static async Task<AppRoleModel> GetViewAppRole(IHubActionTester tester)
    {
        var app = await tester.HubApp();
        var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
        return viewAppRole.ToModel();
    }

    private UserRoleRequest CreateModel(AppUserModel userToEdit, AppRoleModel role)
    {
        return new UserRoleRequest
        {
            UserID = userToEdit.ID,
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

    private async Task<int> AssignRole(IHubActionTester tester, AppUserModel user, ModifierModel modifier, AppRoleModel role)
    {
        var assignRoleTester = tester.Create(hubApi => hubApi.AppUserMaintenance.AssignRole);
        await assignRoleTester.LoginAsAdmin();
        var userRoleID = await assignRoleTester.Execute
        (
            new UserRoleRequest
            {
                UserID = user.ID,
                ModifierID = modifier.ID,
                RoleID = role.ID
            },
            new ModifierKey("General")
        );
        return userRoleID;
    }

    private async Task<AppRoleModel[]> GetExplicitlyAssignedRoles(IHubActionTester sourceTester, AppUserModel user)
    {
        var tester = sourceTester.Create(hubApi => hubApi.AppUser.GetExplicitUserAccess);
        await tester.LoginAsAdmin();
        var app = await sourceTester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var generalUserGroupModifier = await sourceTester.GeneralUserGroupModifier();
        var userAccess = await tester.Execute
        (
            new UserModifierKey
            (
                modifierID: defaultModifier.ID,
                userID: user.ID
            ),
            generalUserGroupModifier
        );
        return userAccess.AssignedRoles;
    }
}