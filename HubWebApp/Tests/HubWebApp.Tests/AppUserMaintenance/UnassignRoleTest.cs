using XTI_HubWebAppApi.AppUserMaintenance;
using XTI_HubWebAppApi.UserList;

namespace HubWebApp.Tests;

internal sealed class UnassignRoleTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        var userToEdit = await addUser(tester, "userToEdit");
        var viewAppRole = await getViewAppRole(tester);
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        await assignRole(tester, userToEdit, viewAppRole);
        var request = new UserRoleRequest
        {
            UserID = userToEdit.ID,
            ModifierID = defaultModifier.ID,
            RoleID = viewAppRole.ID
        };
        AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(request);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        var userToEdit = await addUser(tester, "userToEdit");
        var viewAppRole = await getViewAppRole(tester);
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        await assignRole(tester, userToEdit, viewAppRole);
        var request = new UserRoleRequest
        {
            UserID = userToEdit.ID,
            ModifierID = defaultModifier.ID,
            RoleID = viewAppRole.ID
        };
        var modifier = tester.FakeHubAppModifier();
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldUnassignRoleFromUser()
    {
        var tester = await setup();
        tester.Login(HubInfo.Roles.EditUser);
        var userToEdit = await addUser(tester, "userToEdit");
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
        var model = createModel(userToEdit, viewAppRole);
        await assignRole(tester, userToEdit, viewAppRole);
        var request = new UserRoleRequest
        {
            RoleID = viewAppRole.ID,
            ModifierID = defaultModifier.ID,
            UserID = userToEdit.ID
        };
        var hubAppModifier = await tester.HubAppModifier();
        await tester.Execute(request, hubAppModifier.ModKey());
        var userRoles = await userToEdit.Modifier(hubAppModifier).AssignedRoles();
        Assert.That
        (
            userRoles.Select(r => r.Name()),
            Has.None.EqualTo(HubInfo.Roles.ViewApp),
            "Should unassign role from user"
        );
    }

    private async Task<HubActionTester<UserRoleRequest, EmptyActionResult>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUserMaintenance.UnassignRole);
    }

    private static async Task<AppRole> getViewAppRole(IHubActionTester tester)
    {
        var app = await tester.HubApp();
        var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
        return viewAppRole;
    }

    private UserRoleRequest createModel(AppUser userToEdit, AppRole role)
    {
        return new UserRoleRequest
        {
            UserID = userToEdit.ID,
            RoleID = role.ID
        };
    }

    private async Task<AppUser> addUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        addUserTester.LoginAsAdmin();
        var userID = await addUserTester.Execute
        (
            new AddUserModel
            {
                UserName = userName,
                Password = "Password12345"
            },
            ModifierKey.Default
        );
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var user = await factory.Users.UserByUserName(new AppUserName(userName));
        return user;
    }

    private async Task<int> assignRole(IHubActionTester tester, AppUser user, AppRole role)
    {
        var assignRoleTester = tester.Create(hubApi => hubApi.AppUserMaintenance.AssignRole);
        assignRoleTester.LoginAsAdmin();
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var appContext = tester.Services.GetRequiredService<FakeAppContext>();
        var fakeDefaultModifier = appContext.App().DefaultModifier();
        var hubAppModifier = await tester.HubAppModifier();
        var userRoleID = await assignRoleTester.Execute(new UserRoleRequest
        {
            UserID = user.ID,
            ModifierID = defaultModifier.ID,
            RoleID = role.ID
        }, hubAppModifier.ModKey());
        return userRoleID;
    }
}