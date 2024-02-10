namespace HubWebApp.Tests;

internal sealed class GetUsersWithAnyRoleTest
{
    [Test]
    public async Task ShouldGetUsersWithModifiedRole()
    {
        var tester = await Setup();
        var hubApp = await tester.HubApp();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await AddUser(tester, "someone");
        var viewUserRole = await GetRole(tester, hubApp.ToModel(), HubInfo.Roles.ViewUser);
        await AssignRole(tester, user, hubAppModifier, viewUserRole);
        await tester.LoginAs(GetSystemUserName());
        var users = await tester.Execute
        (
            new SystemGetUsersWithAnyRoleRequest
            (
                0,
                HubInfo.ModCategories.Apps,
                hubAppModifier.ModKey,
                HubInfo.Roles.ViewUser
            )
        );
        Assert.That
        (
            users.Select(u => u.UserName),
            Is.EqualTo(new[] { user.UserName }),
            "Should get users with modified role"
        );
    }

    [Test]
    public async Task ShouldGetUsersWithDefaultRole()
    {
        var tester = await Setup();
        var hubApp = await tester.HubApp();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await AddUser(tester, "someone");
        var viewUserRole = await GetRole(tester, hubApp.ToModel(), HubInfo.Roles.ViewUser);
        var defaultModifier = await hubApp.DefaultModifier();
        await AssignRole(tester, user, defaultModifier.ToModel(), viewUserRole);
        await tester.LoginAs(GetSystemUserName());
        var users = await tester.Execute
        (
            new SystemGetUsersWithAnyRoleRequest
            (
                0,
                HubInfo.ModCategories.Apps,
                hubAppModifier.ModKey,
                HubInfo.Roles.ViewUser
            )
        );
        Assert.That
        (
            users.Select(u => u.UserName),
            Is.EqualTo(new[] { user.UserName }),
            "Should get users with default role"
        );
    }

    [Test]
    public async Task ShouldGetUsersWithDefaultRoleUnlessTheUserHasModifiedRoles()
    {
        var tester = await Setup();
        var hubApp = await tester.HubApp();
        var hubAppModifier = await tester.HubAppModifier();
        var user = await AddUser(tester, "someone");
        var viewUserRole = await GetRole(tester, hubApp.ToModel(), HubInfo.Roles.ViewUser);
        var defaultModifier = await hubApp.DefaultModifier();
        await AssignRole(tester, user, defaultModifier.ToModel(), viewUserRole);
        var viewAppRole = await GetRole(tester, hubApp.ToModel(), HubInfo.Roles.ViewApp);
        await AssignRole(tester, user, hubAppModifier, viewAppRole);
        await tester.LoginAs(GetSystemUserName());
        var users = await tester.Execute
        (
            new SystemGetUsersWithAnyRoleRequest
            (
                installationID: 0,
                modCategoryName: HubInfo.ModCategories.Apps,
                modKey: hubAppModifier.ModKey,
                roleNames: HubInfo.Roles.ViewUser
            )
        );
        Assert.That
        (
            users.Length,
            Is.EqualTo(0),
            "Should not get user with default role when the user has modified roles"
        );
    }

    private async Task<HubActionTester<SystemGetUsersWithAnyRoleRequest, AppUserModel[]>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        var tester = HubActionTester.Create(sp, hubApi => hubApi.System.GetUsersWithAnyRole);
        var systemUser = await AddUser(tester, GetSystemUserName().DisplayText);
        var hubApp = await tester.HubApp();
        var modifier = await hubApp.DefaultModifier();
        var systemUserRole = await GetRole(tester, hubApp.ToModel(), AppRoleName.System);
        await AssignRole(tester, systemUser, modifier.ToModel(), systemUserRole);
        return tester;
    }

    private static AppUserName GetSystemUserName() =>
        new SystemUserName(HubInfo.AppKey, Environment.MachineName).UserName;

    private async Task<AppUserModel> AddUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        await addUserTester.LoginAsAdmin();
        var user = await addUserTester.Execute
        (
            new AddOrUpdateUserRequest
            {
                UserName = userName,
                Password = "Password12345"
            },
            new ModifierKey("General")
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
