using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

internal sealed class MoveAuthenticatorTest
{
    private static readonly AuthenticatorKey authenticatorKey = new("Auth");

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        var sourceUser = await AddUser(tester, "someone");
        var targetUser = await AddUser(tester, "someone.else");
        const string externalUserKey = "external.id";
        await AddUserAuthenticator(tester, sourceUser, externalUserKey);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                new MoveAuthenticatorRequest(authenticatorKey, externalUserKey, targetUser.ID),
                HubInfo.Roles.Admin
            );
    }

    [Test]
    public async Task ShouldMoveAuthenticator()
    {
        var tester = await Setup();
        var sourceUser = await AddUser(tester, "someone");
        var targetUser = await AddUser(tester, "someone.else");
        const string externalUserKey = "external.id";
        await AddUserAuthenticator(tester, sourceUser, externalUserKey);
        await tester.Execute
        (
            new MoveAuthenticatorRequest(authenticatorKey, externalUserKey, targetUser.ID)
        );
        var userFromAuthenticator = await UserFromAuthenticator(tester, externalUserKey);
        Assert.That(userFromAuthenticator, Is.EqualTo(targetUser), "Should move authenticator");
    }

    [Test]
    public async Task ShouldChangeExistingAuthenticatorForTargetUser()
    {
        var tester = await Setup();
        var sourceUser = await AddUser(tester, "someone");
        var targetUser = await AddUser(tester, "someone.else");
        const string externalUserKey1 = "external.id1";
        await AddUserAuthenticator(tester, sourceUser, externalUserKey1);
        const string externalUserKey2 = "external.id2";
        await AddUserAuthenticator(tester, targetUser, externalUserKey2);
        await tester.Execute
        (
            new MoveAuthenticatorRequest(authenticatorKey, externalUserKey1, targetUser.ID)
        );
        var userFromAuthenticator1 = await UserFromAuthenticator(tester, externalUserKey1);
        Assert.That(userFromAuthenticator1, Is.EqualTo(targetUser), "Should change existing authenticator for target user");
        var userFromAuthenticator2 = await UserFromAuthenticator(tester, externalUserKey2);
        Assert.That(userFromAuthenticator2.UserName, Is.EqualTo(AppUserName.Anon), "Should change existing authenticator for target user");
    }

    private async Task<HubActionTester<MoveAuthenticatorRequest, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        var tester = HubActionTester.Create
        (
            services,
            hubApi => hubApi.Authenticators.MoveAuthenticator
        );
        await AddAuthenticator(tester);
        return tester;
    }

    private async Task<AppUserModel> AddUser(IHubActionTester tester, string userName)
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
        return user.ToModel();
    }

    private async Task<AuthenticatorModel> AddAuthenticator(IHubActionTester tester)
    {
        var addAuthTester = tester.Create(hubApi => hubApi.Authenticators.RegisterAuthenticator);
        await addAuthTester.LoginAsAdmin();
        var authenticator = await addAuthTester.Execute(new RegisterAuthenticatorRequest(authenticatorKey));
        return authenticator;
    }

    private async Task<AuthenticatorModel> AddUserAuthenticator(IHubActionTester tester, AppUserModel user, string externalUserKey)
    {
        var addAuthTester = tester.Create(hubApi => hubApi.Authenticators.RegisterUserAuthenticator);
        await addAuthTester.LoginAsAdmin();
        var authenticator = await addAuthTester.Execute
        (
            new RegisterUserAuthenticatorRequest(authenticatorKey, user.ID, externalUserKey)
        );
        return authenticator;
    }

    private async Task<AppUserModel> UserFromAuthenticator(IHubActionTester tester, string externalUserKey)
    {
        var getTester = tester.Create(hubApi => hubApi.Authenticators.UserOrAnonByAuthenticator);
        await getTester.LoginAsAdmin();
        var user = await getTester.Execute
        (
            new UserOrAnonByAuthenticatorRequest(authenticatorKey, externalUserKey)
        );
        return user;
    }
}