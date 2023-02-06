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
        var viewUserRole = await hubApp.Role(HubInfo.Roles.ViewUser);
        await user.Modifier(hubAppModifier).AssignRole(viewUserRole);
        await tester.LoginAs(GetSystemUserName());
        var users = await tester.Execute
        (
            new SystemGetUsersWithAnyRoleRequest
            (
                0,
                HubInfo.ModCategories.Apps,
                hubAppModifier.ToModel().ModKey,
                HubInfo.Roles.ViewUser
            )
        );
        Assert.That
        (
            users.Select(u => u.UserName), 
            Is.EqualTo(new[] { user.ToModel().UserName }),
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
        var viewUserRole = await hubApp.Role(HubInfo.Roles.ViewUser);
        await user.AssignRole(viewUserRole);
        await tester.LoginAs(GetSystemUserName());
        var users = await tester.Execute
        (
            new SystemGetUsersWithAnyRoleRequest
            (
                0,
                HubInfo.ModCategories.Apps,
                hubAppModifier.ToModel().ModKey,
                HubInfo.Roles.ViewUser
            )
        );
        Assert.That
        (
            users.Select(u => u.UserName),
            Is.EqualTo(new[] { user.ToModel().UserName }),
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
        var viewUserRole = await hubApp.Role(HubInfo.Roles.ViewUser);
        await user.AssignRole(viewUserRole);
        var viewAppRole = await hubApp.Role(HubInfo.Roles.ViewApp);
        await user.Modifier(hubAppModifier).AssignRole(viewAppRole);
        await tester.LoginAs(GetSystemUserName());
        var users = await tester.Execute
        (
            new SystemGetUsersWithAnyRoleRequest
            (
                0,
                HubInfo.ModCategories.Apps,
                hubAppModifier.ToModel().ModKey,
                HubInfo.Roles.ViewUser
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
        var systemUserRole = await hubApp.Role(AppRoleName.System);
        await systemUser.AssignRole(systemUserRole);
        return tester;
    }

    private static AppUserName GetSystemUserName()=>
        new SystemUserName(HubInfo.AppKey, Environment.MachineName).Value;

    private async Task<AppUser> AddUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        await addUserTester.LoginAsAdmin();
        await addUserTester.Execute
        (
            new AddOrUpdateUserRequest
            {
                UserName = userName,
                Password = "Password12345"
            },
            new ModifierKey("General")
        );
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var user = await factory.Users.UserByUserName(new AppUserName(userName));
        return user;
    }
}
