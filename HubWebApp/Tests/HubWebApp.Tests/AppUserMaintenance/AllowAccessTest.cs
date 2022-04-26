﻿using XTI_HubAppApi.UserList;

namespace HubWebApp.Tests;

internal sealed class AllowAccessTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var user = await addUser(tester, "someone");
        var modifier = await tester.HubAppModifier();
        var request = new UserModifierKey
        {
            UserID = user.ID.Value,
            ModifierID = modifier.ID.Value
        };
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(request);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var user = await addUser(tester, "someone");
        var modifier = await tester.HubAppModifier();
        var request = new UserModifierKey
        {
            UserID = user.ID.Value,
            ModifierID = modifier.ID.Value
        };
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                tester.FakeHubAppModifier(),
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldRemoveDenyAccessRole()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var user = await addUser(tester, "someone");
        var hubApp = await tester.HubApp();
        var denyAccessRole = await hubApp.Role(AppRoleName.DenyAccess);
        var modifier = await tester.HubAppModifier();
        await user.Modifier(modifier).AssignRole(denyAccessRole);
        var request = new UserModifierKey
        {
            UserID = user.ID.Value,
            ModifierID = modifier.ID.Value
        };
        await tester.Execute(request, modifier.ModKey());
        var roles = await user.Modifier(modifier).AssignedRoles();
        Assert.That
        (
            roles.Select(r => r.Name()),
            Is.EqualTo(new AppRoleName[0]),
            "Should remove deny access role"
        );
    }

    private async Task<HubActionTester<UserModifierKey, EmptyActionResult>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUserMaintenance.AllowAccess);
    }

    private async Task<AppUser> addUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
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