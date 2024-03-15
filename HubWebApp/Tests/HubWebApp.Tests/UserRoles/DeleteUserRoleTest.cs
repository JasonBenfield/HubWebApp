using Microsoft.EntityFrameworkCore;
using XTI_HubDB.EF;

namespace HubWebApp.Tests;

internal sealed class DeleteUserRoleTest
{
    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        var userToEdit = await AddUser(tester, "userToEdit");
        var app = await GetApp(tester, HubInfo.AppKey);
        var defaultModifier = await GetDefaultModifier(tester, app);
        var role = await GetRole(tester, app, HubInfo.Roles.ViewApp);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                async () =>
                {
                    await AssignRole(tester, userToEdit, role, defaultModifier);
                    var userRoleID = await GetUserRoleID(tester, userToEdit, defaultModifier, role);
                    return new UserRoleIDRequest(userRoleID);
                },
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDeniedToUserGroup()
    {
        var tester = await Setup();
        var userToEdit = await AddUser(tester, "userToEdit");
        var app = await GetApp(tester, HubInfo.AppKey);
        var modCategory = await GetModCategory(tester, app, HubInfo.ModCategories.UserGroups);
        var modifier = await GetModifier(tester, app, modCategory, new ModifierKey("XTI"));
        await tester.Login(modifier, HubInfo.Roles.EditUser);
        var role = await GetRole(tester, app, HubInfo.Roles.ViewApp);
        var defaultModifier = await GetDefaultModifier(tester, app);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                async () =>
                {
                    await AssignRole(tester, userToEdit, role, defaultModifier);
                    var userRoleID = await GetUserRoleID(tester, userToEdit, defaultModifier, role);
                    return new UserRoleIDRequest(userRoleID);
                },
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldDeleteUserRole()
    {
        var tester = await Setup();
        await tester.Login(HubInfo.Roles.EditUser);
        var userToEdit = await AddUser(tester, "userToEdit");
        var app = await GetApp(tester, HubInfo.AppKey);
        var defaultModifier = await GetDefaultModifier(tester, app);
        var role = await GetRole(tester, app, HubInfo.Roles.ViewApp);
        await AssignRole(tester, userToEdit, role, defaultModifier);
        var userRoleID = await GetUserRoleID(tester, userToEdit, defaultModifier, role);
        await tester.Execute(new UserRoleIDRequest(userRoleID));
        var afterDeleteUserRoleID = await GetUserRoleID(tester, userToEdit, defaultModifier, role);
        Assert.That(afterDeleteUserRoleID, Is.EqualTo(0), "Should delete user role");
    }

    private async Task<HubActionTester<UserRoleIDRequest, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.UserRoles.DeleteUserRole);
    }

    private async Task<AppUserModel> AddUser(IHubActionTester sourceTester, string userName)
    {
        var addUserTester = sourceTester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        await addUserTester.LoginAsAdmin();
        var modifier = await sourceTester.GeneralUserGroupModifier();
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

    private static async Task<AppModel> GetApp(IHubActionTester sourceTester, AppKey appKey)
    {
        var tester = sourceTester.Create(api => api.Apps.GetApps);
        await tester.LoginAsAdmin();
        var apps = await tester.Execute(new EmptyRequest());
        return apps.First(a => a.AppKey.Equals(appKey));
    }

    private static async Task<AppRoleModel> GetRole(IHubActionTester sourceTester, AppModel app, AppRoleName roleName)
    {
        var tester = sourceTester.Create(api => api.App.GetRoles);
        await tester.LoginAsAdmin();
        var roles = await tester.Execute(new EmptyRequest(), app.PublicKey);
        return roles.First(r => r.Name.Equals(roleName));
    }

    private static async Task<ModifierModel> GetDefaultModifier(IHubActionTester sourceTester, AppModel app)
    {
        var tester = sourceTester.Create(api => api.App.GetDefaultModifier);
        await tester.LoginAsAdmin();
        var modifier = await tester.Execute(new EmptyRequest(), app.PublicKey);
        return modifier;
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

    private static async Task AssignRole(IHubActionTester sourceTester, AppUserModel user, AppRoleModel role, ModifierModel modifier)
    {
        var assignTester = sourceTester.Create(hubApi => hubApi.AppUserMaintenance.AssignRole);
        await assignTester.LoginAsAdmin();
        await assignTester.Execute
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

    private static Task<int> GetUserRoleID(IHubActionTester sourceTester, AppUserModel user, ModifierModel modifier, AppRoleModel role)
    {
        var db = sourceTester.Services.GetRequiredService<HubDbContext>();
        return db.UserRoles.Retrieve()
            .Where(ur => ur.UserID == user.ID && ur.ModifierID == modifier.ID && ur.RoleID == role.ID)
            .Select(ur => ur.ID)
            .FirstOrDefaultAsync();
    }
}