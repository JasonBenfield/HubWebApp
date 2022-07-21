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
            UserID = userToEdit.ToModel().ID,
            ModifierID = defaultModifier.ID,
            RoleID = viewAppRole.ToModel().ID
        };
        await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(request);
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
            UserID = userToEdit.ToModel().ID,
            ModifierID = defaultModifier.ID,
            RoleID = viewAppRole.ToModel().ID
        };
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                new[] { HubInfo.Roles.ViewApp },
                generalUserGroupModifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldUnassignRoleFromUser()
    {
        var tester = await setup();
        await tester.Login(HubInfo.Roles.EditUser);
        var userToEdit = await addUser(tester, "userToEdit");
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
        var model = createModel(userToEdit, viewAppRole);
        await assignRole(tester, userToEdit, viewAppRole);
        var request = new UserRoleRequest
        {
            RoleID = viewAppRole.ToModel().ID,
            ModifierID = defaultModifier.ID,
            UserID = userToEdit.ToModel().ID
        };
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        await tester.Execute(request, generalUserGroupModifier);
        var hubAppModifier = await tester.HubAppModifier();
        var userRoles = await userToEdit.Modifier(hubAppModifier).AssignedRoles();
        Assert.That
        (
            userRoles.Select(r => r.ToModel().Name),
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
            UserID = userToEdit.ToModel().ID,
            RoleID = role.ToModel().ID
        };
    }

    private async Task<AppUser> addUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        await addUserTester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var userID = await addUserTester.Execute
        (
            new AddUserModel
            {
                UserName = userName,
                Password = "Password12345"
            },
            modifier
        );
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var user = await factory.Users.UserByUserName(new AppUserName(userName));
        return user;
    }

    private async Task<int> assignRole(IHubActionTester tester, AppUser user, AppRole role)
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
                UserID = user.ToModel().ID,
                ModifierID = defaultModifier.ID,
                RoleID = role.ToModel().ID
            },
            generalUserGroupModifier
        );
        return userRoleID;
    }
}