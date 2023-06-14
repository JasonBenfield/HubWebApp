using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

internal sealed class UserOrAnonByAuthenticatorTest
{
    private static readonly AuthenticatorKey authenticatorKey = new("Auth");

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        var user = await AddUser(tester, "someone");
        const string externalUserKey = "external.id";
        await AddUserAuthenticator(tester, user, externalUserKey);
        var request = new UserOrAnonByAuthenticatorRequest(authenticatorKey, externalUserKey);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                HubInfo.Roles.Admin
            );
    }

    [Test]
    public async Task ShouldGetUserByAuthenticator()
    {
        var tester = await Setup();
        var user = await AddUser(tester, "someone");
        const string externalUserKey = "external.id";
        await AddUserAuthenticator(tester, user, externalUserKey);
        var userFromAuthenticator = await tester.Execute
        (
            new UserOrAnonByAuthenticatorRequest(authenticatorKey, externalUserKey)
        );
        Assert.That(userFromAuthenticator, Is.EqualTo(user), "Should get user by authenticator");
    }

    [Test]
    public async Task ShouldGetAnonUser_WhenAuthenticatorIsNotFound()
    {
        var tester = await Setup();
        var user = await AddUser(tester, "someone");
        const string externalUserKey = "external.id";
        await AddUserAuthenticator(tester, user, externalUserKey);
        var userFromAuthenticator = await tester.Execute
        (
            new UserOrAnonByAuthenticatorRequest(authenticatorKey, "no_such_user")
        );
        Assert.That
        (
            userFromAuthenticator.UserName, 
            Is.EqualTo(AppUserName.Anon), 
            "Should get anon user when external user key is not found"
        );
    }

    private async Task<HubActionTester<UserOrAnonByAuthenticatorRequest, AppUserModel>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        var tester = HubActionTester.Create
        (
            services,
            hubApi => hubApi.Authenticators.UserOrAnonByAuthenticator
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
}