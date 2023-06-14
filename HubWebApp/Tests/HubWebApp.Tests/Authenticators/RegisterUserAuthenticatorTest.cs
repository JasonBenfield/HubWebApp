using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

internal sealed class RegisterUserAuthenticatorTest
{
    private static readonly AuthenticatorKey authenticatorKey = new("Auth");

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        var user = await AddUser(tester, "someone");
        var request = CreateRequest(user);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                HubInfo.Roles.Admin
            );
    }

    [Test]
    public async Task ShouldAddUserAuthenticator()
    {
        var tester = await Setup();
        var user = await AddUser(tester, "someone");
        var request = CreateRequest(user);
        await tester.Execute(request);
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var userAuthenticators = await db.UserAuthenticators.Retrieve()
            .ToArrayAsync();
        Assert.That(userAuthenticators.Length, Is.EqualTo(1), "Should add authenticator to user");
        var authenticator = await db.Authenticators.Retrieve()
            .FirstAsync(a => a.AuthenticatorKey == authenticatorKey.Value);
        Assert.That(userAuthenticators[0].AuthenticatorID, Is.EqualTo(authenticator.ID));
        Assert.That(userAuthenticators[0].UserID, Is.EqualTo(user.ID));
        Assert.That(userAuthenticators[0].ExternalUserKey, Is.EqualTo("external.id"));
    }

    [Test]
    public async Task ShouldAddUserAuthenticatorOnlyOnce()
    {
        var tester = await Setup();
        var user = await AddUser(tester, "someone");
        var request = CreateRequest(user);
        await tester.Execute(request);
        await tester.Execute(request);
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var userAuthenticators = await db.UserAuthenticators.Retrieve().ToArrayAsync();
        Assert.That(userAuthenticators.Length, Is.EqualTo(1), "Should add authenticator to user only once");
    }

    [Test]
    public async Task ShouldUpdateExternalKey_WhenUserAuthenticatorAlreadyExists()
    {
        var tester = await Setup();
        var user = await AddUser(tester, "someone");
        var request = CreateRequest(user);
        await tester.Execute(request);
        request.ExternalUserKey = "external.id2";
        await tester.Execute(request);
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var userAuthenticators = await db.UserAuthenticators.Retrieve().ToArrayAsync();
        Assert.That
        (
            userAuthenticators[0].ExternalUserKey,
            Is.EqualTo("external.id2"),
            "Should update external user key"
        );
    }

    [Test]
    public async Task ShouldNotAllowTheSameExternalUserKeyForDifferentUsers()
    {
        var tester = await Setup();
        var user1 = await AddUser(tester, "someone");
        var request = CreateRequest(user1);
        await tester.Execute(request);
        var user2 = await AddUser(tester, "someone.else");
        request.UserID = user2.ID;
        var ex = Assert.ThrowsAsync<AppException>(() => tester.Execute(request));
        Assert.That
        (
            ex?.Message, 
            Is.EqualTo
            (
                string.Format
                (
                    AppErrors.AuthenticatorExistsForDifferentUser, 
                    authenticatorKey.DisplayText, 
                    request.ExternalUserKey, 
                    user1.ID
                )
            )
        );
    }

    private async Task<HubActionTester<RegisterUserAuthenticatorRequest, AuthenticatorModel>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        var tester = HubActionTester.Create
        (
            services,
            hubApi => hubApi.Authenticators.RegisterUserAuthenticator
        );
        await AddAuthenticator(tester);
        return tester;
    }

    private async Task<AppUserModel> AddUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        await addUserTester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
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

    private static RegisterUserAuthenticatorRequest CreateRequest(AppUserModel user) =>
        new RegisterUserAuthenticatorRequest
        (
            authenticatorKey,
            user.ID,
            "external.id"
        );

    private async Task<AuthenticatorModel> AddAuthenticator(IHubActionTester tester)
    {
        var addAuthTester = tester.Create(hubApi => hubApi.Authenticators.RegisterAuthenticator);
        await addAuthTester.LoginAsAdmin();
        var authenticator = await addAuthTester.Execute(new RegisterAuthenticatorRequest(authenticatorKey));
        return authenticator;
    }
}