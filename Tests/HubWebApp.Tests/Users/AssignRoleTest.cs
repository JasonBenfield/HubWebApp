using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Fakes;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.AppUserMaintenance;
using XTI_HubAppApi.UserList;

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
        AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(model);
    }

    [Test]
    public async Task ShouldThrowError_WhenModifierIsNotAssignedToUser()
    {
        var tester = await setup();
        var userToEdit = await addUser(tester, "userToEdit");
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await getViewAppRole(tester);
        var model = createModel(userToEdit, defaultModifier, viewAppRole);
        var modifier = await tester.FakeHubAppModifier();
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsNotAssignedToUser_ButRoleIsAssignedToUser
            (
                model,
                HubInfo.Roles.ViewApp,
                modifier
            );
    }

    [Test]
    public async Task ShouldAssignRoleToUser()
    {
        var tester = await setup();
        tester.Login(HubInfo.Roles.EditUser);
        var userToEdit = await addUser(tester, "userToEdit");
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
        var model = createModel(userToEdit, defaultModifier, viewAppRole);
        var hubAppModifier = await tester.HubAppModifier();
        await tester.Execute(model, hubAppModifier.ModKey());
        var userRoles = await userToEdit.AssignedRoles(hubAppModifier);
        Assert.That
        (
            userRoles.Select(r => r.Name()),
            Has.One.EqualTo(HubInfo.Roles.ViewApp),
            "Should assign role to user"
        );
    }

    [Test]
    public async Task ShouldReturnUserRoleID()
    {
        var tester = await setup();
        tester.Login(HubInfo.Roles.EditUser);
        var userToEdit = await addUser(tester, "userToEdit");
        var app = await tester.HubApp();
        var defaultModifier = await app.DefaultModifier();
        var viewAppRole = await app.Role(HubInfo.Roles.ViewApp);
        var model = createModel(userToEdit, defaultModifier, viewAppRole);
        var hubAppModifier = await tester.HubAppModifier();
        var userRoleID = await tester.Execute(model, hubAppModifier.ModKey());
        var userRoles = await userToEdit.AssignedRoles(hubAppModifier);
        Assert.That
        (
            userRoles.Select(r => r.ID),
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
            UserID = userToEdit.ID.Value,
            ModifierID = modifier.ID.Value,
            RoleID = role.ID.Value
        };
    }

    private async Task<AppUser> addUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddUser);
        addUserTester.LoginAsAdmin();
        var userID = await addUserTester.Execute(new AddUserModel
        {
            UserName = userName,
            Password = "Password12345"
        });
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var user = await factory.Users.UserByUserName(new AppUserName(userName));
        return user;
    }
}