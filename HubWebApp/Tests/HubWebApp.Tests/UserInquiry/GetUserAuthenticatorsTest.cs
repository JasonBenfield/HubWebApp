namespace HubWebApp.Tests;

internal sealed class GetUserAuthenticatorsTest
{
    private static readonly AuthenticatorKey authenticatorKey = new AuthenticatorKey("Auth");

    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var user = await AddUser(tester, "User1");
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(new AppUserIDRequest(user.ID));
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var user = await AddUser(tester, "User1");
        var modifier = await tester.GeneralUserGroupModifier();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                new AppUserIDRequest(user.ID),
                modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.ViewUser
            );
    }

    [Test]
    public async Task ShouldGetUserAuthenticators()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var user = await AddUser(tester, "User1");
        const string externalUserKey = "ExternalUser1";
        await RegisterUserAuthenticator(tester, user.ID, externalUserKey);
        var modifier = await tester.GeneralUserGroupModifier();
        var userAuthenticators = await tester.Execute(new AppUserIDRequest(user.ID), modifier);
        Assert.That
        (
            userAuthenticators.Select(ua => ua.ExternalUserID).ToArray(),
            Is.EquivalentTo(new[] { externalUserKey }),
            "Should get user authenticators"
        );
        Assert.That
        (
            userAuthenticators.Select(ua => ua.Authenticator.AuthenticatorKey).ToArray(),
            Is.EquivalentTo(new[] { authenticatorKey }),
            "Should get user authenticators"
        );
    }

    private async Task<HubActionTester<AppUserIDRequest, UserAuthenticatorModel[]>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        var tester = HubActionTester.Create(sp, hubApi => hubApi.UserInquiry.GetUserAuthenticators);
        await AddAuthenticator(tester);
        return tester;
    }

    private async Task<AppUserModel> AddUser(IHubActionTester tester, string userName)
    {
        var form = new AddUserForm();
        form.UserName.SetValue(userName);
        form.Password.SetValue("Password1234");
        form.Confirm.SetValue("Password1234");
        form.PersonName.SetValue(userName);
        form.Email.SetValue($"{userName}@example.com");
        var addTester = tester.Create(api => api.Users.AddUser);
        var modifier = await tester.GeneralUserGroupModifier();
        var user = await addTester.Execute(form, modifier);
        return user;
    }

    private async Task<AuthenticatorModel> AddAuthenticator(IHubActionTester tester)
    {
        var addAuthTester = tester.Create(hubApi => hubApi.Authenticators.RegisterAuthenticator);
        await addAuthTester.LoginAsAdmin();
        var authenticator = await addAuthTester.Execute(new RegisterAuthenticatorRequest(authenticatorKey));
        return authenticator;
    }

    private Task RegisterUserAuthenticator(IHubActionTester tester, int userID, string externalUserKey)
    {
        var registerTester = tester.Create(api => api.Authenticators.RegisterUserAuthenticator);
        return registerTester.Execute
        (
            new RegisterUserAuthenticatorRequest(authenticatorKey, userID, externalUserKey)
        );
    }
}
