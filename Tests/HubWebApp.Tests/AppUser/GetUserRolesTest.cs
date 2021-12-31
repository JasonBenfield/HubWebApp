using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using XTI_App.Abstractions;
using XTI_Hub;
using XTI_HubAppApi.AppUserInquiry;
using XTI_HubAppApi.UserList;

namespace HubWebApp.Tests;

internal sealed class GetUserRolesTest
{
    [Test]
    public async Task ShouldGetUserRoles()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var hubApp = await tester.HubApp();
        var hubAppModifier = await tester.HubAppModifier();
        var adminRole = await tester.AdminRole();
        var user = await addUser(tester, "someone"); 
        var viewUserRole = await hubApp.Role(HubInfo.Roles.ViewUser);
        await user.AddRole(viewUserRole, hubAppModifier);
        var request = new GetUserRolesRequest
        {
            UserID = user.ID.Value,
            ModifierID = hubAppModifier.ID.Value
        };
        var roles = await tester.Execute(request, hubAppModifier.ModKey());
        Assert.That
        (
            roles.Select(r => new AppRoleName(r.Name)), 
            Has.One.EqualTo(HubInfo.Roles.ViewUser), 
            "Should get user roles"
        );
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

    private async Task<HubActionTester<GetUserRolesRequest, AppRoleModel[]>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUser.GetUserRoles);
    }
}