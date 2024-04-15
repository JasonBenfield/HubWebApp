namespace HubWebApp.Tests;

internal sealed class GetUserAccessTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await AddUser(tester, "someone");
        var request = new UserModifierKey
        (
            userID: user.ID,
            modifierID: hubAppModifier.ID
        );
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(request);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
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
    public async Task ShouldGetUserRolesForModifier()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var hubApp = await tester.HubApp();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await AddUser(tester, "someone");
        var viewUserRole = await GetRole(tester, hubApp.ToModel(), HubInfo.Roles.ViewUser);
        var modCategory = await GetModCategory(tester, hubApp.ToModel(), HubInfo.ModCategories.Apps);
        var modifier = await GetModifier(tester, hubApp.ToModel(), modCategory, hubApp.ToModel().PublicKey);
        await AssignRole(tester, user, modifier, viewUserRole);
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = hubAppModifier.ID
        };
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        var userAccess = await tester.Execute(request, generalUserGroupModifier);
        Assert.That
        (
            userAccess.AssignedRoles.Select(r => r.Name),
            Has.One.EqualTo(HubInfo.Roles.ViewUser),
            "Should get user roles for modifier"
        );
    }

    [Test]
    public async Task ShouldReturnAccessDenied_WhenAccessIsDenied()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await AddUser(tester, "someone");
        await DenyAccess(tester, user, hubAppModifier);
        var request = new UserModifierKey
        (
            userID: user.ID,
            modifierID: hubAppModifier.ID
        );
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        var userAccess = await tester.Execute(request, generalUserGroupModifier);
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
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await AddUser(tester, "someone");
        await DenyAccess(tester, user, hubAppModifier);
        var request = new UserModifierKey
        (
            userID: user.ID,
            modifierID: hubAppModifier.ID
        );
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        var userAccess = await tester.Execute(request, generalUserGroupModifier);
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
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await AddUser(tester, "someone");
        var hubApp = await tester.HubApp();
        var defaultModifier = await hubApp.DefaultModifier();
        var role = await GetRole(tester, hubApp.ToModel(), HubInfo.Roles.EditUser);
        var modCategory = await GetModCategory(tester, hubApp.ToModel(), HubInfo.ModCategories.Default);
        var modifier = await GetModifier(tester, hubApp.ToModel(), modCategory, ModifierKey.Default);
        await AssignRole(tester, user, modifier, role);
        var request = new UserModifierKey
        {
            UserID = user.ID,
            ModifierID = hubAppModifier.ID
        };
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        var userAccess = await tester.Execute(request, generalUserGroupModifier);
        Assert.That
        (
            userAccess.AssignedRoles.Select(r => r.Name),
            Has.None.EqualTo(HubInfo.Roles.EditUser),
            "Should only include explicitly assigned roles"
        );
    }

    private async Task<HubActionTester<UserModifierKey, UserAccessModel>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUser.GetExplicitUserAccess);
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

    private static async Task<ModifierCategoryModel> GetModCategory(IHubActionTester sourceTester, AppModel app, ModifierCategoryName modCategoryName)
    {
        var tester = sourceTester.Create(api => api.App.GetModifierCategories);
        await tester.LoginAsAdmin();
        var modCategories = await tester.Execute(new EmptyRequest(), app.PublicKey);
        return modCategories.FirstOrDefault(mc => mc.Name.Equals(modCategoryName)) ?? new();
    }

    private static async Task<ModifierModel> GetModifier(IHubActionTester sourceTester, AppModel app, ModifierCategoryModel modCategory, ModifierKey modKey)
    {
        var tester = sourceTester.Create(api => api.ModCategory.GetModifiers);
        await tester.LoginAsAdmin();
        var modifiers = await tester.Execute(modCategory.ID, app.PublicKey);
        return modifiers.FirstOrDefault(m => m.ModKey.Equals(modKey)) ?? new();
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

    private async Task DenyAccess(IHubActionTester tester, AppUserModel user, ModifierModel modifier)
    {
        var denyAccessTester = tester.Create(hubApi => hubApi.AppUserMaintenance.DenyAccess);
        await denyAccessTester.LoginAsAdmin();
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        await denyAccessTester.Execute
        (
            new UserModifierKey
            {
                UserID = user.ID,
                ModifierID = modifier.ID
            },
            generalUserGroupModifier
        );
    }

}