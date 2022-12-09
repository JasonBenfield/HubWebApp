using XTI_HubWebAppApi.AppUserMaintenance;
using XTI_HubWebAppApi.UserList;

namespace HubWebApp.Tests;

internal sealed class AssignRoleTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        var userToEdit = await addUser(tester, "userToEdit");
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await getViewAppRole(tester);
        var model = createModel(userToEdit, defaultModifier, viewAppRole);
        await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(model);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        var userToEdit = await addUser(tester, "userToEdit");
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await getViewAppRole(tester);
        var model = createModel(userToEdit, defaultModifier, viewAppRole);
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                model,
                new AppRoleName[] { HubInfo.Roles.ViewApp },
                generalUserGroupModifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldAssignRoleToUser()
    {
        var tester = await setup();
        var userToEdit = await addUser(tester, "userToEdit");
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        await tester.Login(new[] { HubInfo.Roles.ViewApp }, generalUserGroupModifier, HubInfo.Roles.EditUser);
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
        var model = createModel(userToEdit, defaultModifier, viewAppRole);
        var hubAppModifier = await tester.HubAppModifier();
        await tester.Execute(model, generalUserGroupModifier);
        var userRoles = await userToEdit.Modifier(hubAppModifier).AssignedRoles();
        Assert.That
        (
            userRoles.Select(r => r.ToModel().Name),
            Has.One.EqualTo(HubInfo.Roles.ViewApp),
            "Should assign role to user"
        );
    }

    [Test]
    public async Task ShouldReturnUserRoleID()
    {
        var tester = await setup();
        await tester.Login(HubInfo.Roles.EditUser);
        var userToEdit = await addUser(tester, "userToEdit");
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
        var model = createModel(userToEdit, defaultModifier, viewAppRole);
        var hubAppModifier = await tester.HubAppModifier();
        var generalUserGroupModifier = await tester.GeneralUserGroupModifier();
        var userRoleID = await tester.Execute(model, generalUserGroupModifier);
        var userRoles = await userToEdit.Modifier(hubAppModifier).AssignedRoles();
        Assert.That
        (
            userRoles.Select(r => r.ToModel().ID),
            Has.One.EqualTo(userRoleID),
            "Should return user role ID"
        );
    }

    private async Task<HubActionTester<UserRoleRequest, int>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUserMaintenance.AssignRole);
    }

    private static async Task<AppRole> getViewAppRole(IHubActionTester tester)
    {
        var app = await tester.HubApp();
        var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
        return viewAppRole;
    }

    private UserRoleRequest createModel(AppUser userToEdit, Modifier modifier, AppRole role)
    {
        return new UserRoleRequest
        {
            UserID = userToEdit.ToModel().ID,
            ModifierID = modifier.ID,
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
            new AddOrUpdateUserRequest
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
}