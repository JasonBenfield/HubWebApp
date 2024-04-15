using HubWebApp.Fakes;
using System.Security.Claims;
using XTI_WebApp.Api;

namespace HubWebApp.Tests;

internal sealed class ExternalLoginTest
{
    private static readonly AuthenticatorKey authenticatorKey = new AuthenticatorKey("Auth");

    [Test]
    public async Task ShouldAuthenticateUser()
    {
        var tester = await Setup();
        var user = await AddUser(tester, "someone");
        const string externalUserKey = "external.id";
        await AddUserAuthenticator(tester, user.ToModel(), externalUserKey);
        var loginResult = await ExternalAuthKey(tester, externalUserKey);
        var returnKey = await LoginReturnKey(tester, "/Home");
        var request = new AuthenticatedLoginRequest
        (
            authKey: loginResult.AuthKey,
            authID: loginResult.AuthID,
            returnKey: returnKey
        );
        await tester.Execute(request);
        var access = tester.Services.GetRequiredService<FakeAccessForLogin>();
        Assert.That
        (
            access.Claims,
            Has.One.EqualTo
            (
                new Claim("UserName", user.ToModel().UserName.Value)
            )
            .Using<Claim>((x, y) => x.Type == y.Type && x.Value == y.Value),
            "Should authenticate user"
        );
    }

    private async Task<HubActionTester<AuthenticatedLoginRequest, WebRedirectResult>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        var tester = HubActionTester.Create
        (
            services,
            hubApi => hubApi.Auth.Login
        );
        await AddAuthenticator(tester);
        return tester;
    }

    private async Task<AppUser> AddUser(IHubActionTester tester, string userName)
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

    private async Task<AuthenticatedLoginResult> ExternalAuthKey(IHubActionTester tester, string externalUserKey)
    {
        var externalAuthKeyTester = tester.Create(hubApi => hubApi.ExternalAuth.ExternalAuthKey);
        await externalAuthKeyTester.LoginAsAdmin();
        var loginResult = await externalAuthKeyTester.Execute
        (
            new ExternalAuthKeyModel(authenticatorKey, externalUserKey)
        );
        return loginResult;
    }

    private async Task<string> LoginReturnKey(IHubActionTester tester, string returnUrl)
    {
        var loginReturnKeyTester = tester.Create(hubApi => hubApi.Auth.LoginReturnKey);
        await loginReturnKeyTester.LoginAsAdmin();
        var result = await loginReturnKeyTester.Execute(new LoginReturnModel
        {
            ReturnUrl = returnUrl
        });
        return result;
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
        var authenticator = await addAuthTester.Execute(new RegisterUserAuthenticatorRequest(authenticatorKey, user.ID, externalUserKey));
        return authenticator;
    }
}