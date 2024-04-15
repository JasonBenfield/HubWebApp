using XTI_Core.Fakes;
using XTI_WebApp.Abstractions;

namespace HubWebApp.Tests;

internal sealed class DeactivateUsersTest
{
    [Test]
    public async Task ShouldDeactivateUsersWhoHaveNotLoggedIn()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var userName = new AppUserName("xartogg");
        var password = Guid.NewGuid().ToString();
        var user = await AddUser(tester, userName, password);
        await Login(tester, userName, password);
        var clock = tester.Services.GetRequiredService<FakeClock>();
        var hubOptions = tester.Services.GetRequiredService<HubWebAppOptions>();
        clock.Add(TimeSpan.FromDays(hubOptions.Login.DaysBeforeDeactivation + 1));
        await LoginAdmin(tester);
        await tester.Execute(new EmptyRequest());
        user = await GetUser(tester, user);
        Assert.That(user.IsActive(), Is.False, "Should deactivate user who has not logged in recently");
    }

    [Test]
    public async Task ShouldDeactivateNewUsersWhoWereNotCreatedRecentlyAndHaveNotLoggedIn()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var userName = new AppUserName("xartogg");
        var password = Guid.NewGuid().ToString();
        var user = await AddUser(tester, userName, password);
        var clock = tester.Services.GetRequiredService<FakeClock>();
        var hubOptions = tester.Services.GetRequiredService<HubWebAppOptions>();
        clock.Add(TimeSpan.FromDays(hubOptions.Login.DaysBeforeDeactivation + 1));
        await LoginAdmin(tester);
        await tester.Execute(new EmptyRequest());
        user = await GetUser(tester, user);
        Assert.That(user.IsActive(), Is.False, "Should deactivate user who was not created recently and never logged in");
    }

    private async Task LoginAdmin(HubActionTester<EmptyRequest, EmptyActionResult> tester)
    {
        await Login(tester, new AppUserName("hubadmin"), "Password12345");
    }

    [Test]
    public async Task ShouldNotDeactivateRecentlyCreatedNewUsersWhoHaveNotLoggedIn()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var userName = new AppUserName("xartogg");
        var password = Guid.NewGuid().ToString();
        var user = await AddUser(tester, userName, password);
        var clock = tester.Services.GetRequiredService<FakeClock>();
        var hubOptions = tester.Services.GetRequiredService<HubWebAppOptions>();
        clock.Add(TimeSpan.FromDays(hubOptions.Login.DaysBeforeDeactivation - 1));
        await LoginAdmin(tester);
        await tester.Execute(new EmptyRequest());
        user = await GetUser(tester, user);
        Assert.That(user.IsActive(), Is.True, "Should not deactivate recently created new user who have not logged in");
    }

    [Test]
    public async Task ShouldNotDeactivateUsersWhoHaveLoggedIn()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var userName = new AppUserName("xartogg");
        var password = Guid.NewGuid().ToString();
        var user = await AddUser(tester, userName, password);
        await Login(tester, userName, password);
        var clock = tester.Services.GetRequiredService<FakeClock>();
        var hubOptions = tester.Services.GetRequiredService<HubWebAppOptions>();
        clock.Add(TimeSpan.FromDays(hubOptions.Login.DaysBeforeDeactivation - 1));
        await Login(tester, userName, password);
        clock.Add(TimeSpan.FromDays(2));
        await LoginAdmin(tester);
        await tester.Execute(new EmptyRequest());
        user = await GetUser(tester, user);
        Assert.That(user.IsActive(), Is.True, "Should not deactivate user who has logged in recently");
    }

    [Test]
    public async Task ShouldNotDeactivateUsersWhoAuthenticate()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var userName = new AppUserName("xartogg");
        var password = Guid.NewGuid().ToString();
        var user = await AddUser(tester, userName, password);
        await ApiAuthenticate(tester, userName, password);
        var clock = tester.Services.GetRequiredService<FakeClock>();
        var hubOptions = tester.Services.GetRequiredService<HubWebAppOptions>();
        clock.Add(TimeSpan.FromDays(hubOptions.Login.DaysBeforeDeactivation - 1));
        await ApiAuthenticate(tester, userName, password);
        clock.Add(TimeSpan.FromDays(2));
        await LoginAdmin(tester);
        await tester.Execute(new EmptyRequest());
        user = await GetUser(tester, user);
        Assert.That(user.IsActive(), Is.True, "Should not deactivate user who has logged in recently with API authentication");
    }

    [Test]
    public async Task ShouldNotDeactivateUsersWhoAuthenticateExternally()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var userName = new AppUserName("xartogg");
        var password = Guid.NewGuid().ToString();
        var user = await AddUser(tester, userName, password);
        await AddAuthenticator(tester);
        const string externalUserKey = "external.id";
        await AddUserAuthenticator(tester, user, externalUserKey);
        await ExternalAuth(tester, externalUserKey);
        var clock = tester.Services.GetRequiredService<FakeClock>();
        var hubOptions = tester.Services.GetRequiredService<HubWebAppOptions>();
        clock.Add(TimeSpan.FromDays(hubOptions.Login.DaysBeforeDeactivation - 1));
        await ExternalAuth(tester, externalUserKey);
        clock.Add(TimeSpan.FromDays(2));
        await LoginAdmin(tester);
        await tester.Execute(new EmptyRequest());
        user = await GetUser(tester, user);
        Assert.That(user.IsActive(), Is.True, "Should not deactivate user who has logged in recently with external authentication");
    }

    private async Task<HubActionTester<EmptyRequest, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        var options = sp.GetRequiredService<HubWebAppOptions>();
        options.Login.DaysBeforeDeactivation = 90;
        return HubActionTester.Create(sp, hubApi => hubApi.Periodic.DeactivateUsers);
    }

    private async Task<AppUserModel> AddUser(IHubActionTester tester, AppUserName userName, string password)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        await addUserTester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var user = await addUserTester.Execute
        (
            new AddOrUpdateUserRequest(userName, password, new PersonName(""), ""),
            modifier
        );
        return user;
    }

    private async Task<AuthenticatedLoginResult> Login(IHubActionTester tester, AppUserName userName, string password)
    {
        var loginTester = tester.Create(hubApi => hubApi.Auth.VerifyLogin);
        var form = new VerifyLoginForm();
        form.UserName.SetValue(userName.DisplayText);
        form.Password.SetValue(password);
        var loginResult = await loginTester.Execute(form);
        return loginResult;
    }

    private async Task<LoginResult> ApiAuthenticate(IHubActionTester tester, AppUserName userName, string password)
    {
        var authTester = tester.Create(hubApi => hubApi.AuthApi.Authenticate);
        var form = new VerifyLoginForm();
        form.UserName.SetValue(userName.DisplayText);
        form.Password.SetValue(password);
        var loginResult = await authTester.Execute
        (
            new AuthenticateRequest(userName, password)
        );
        return loginResult;
    }

    private async Task<AuthenticatedLoginResult> ExternalAuth(IHubActionTester tester, string externalUserKey)
    {
        var externalAuthKeyTester = tester.Create(hubApi => hubApi.ExternalAuth.ExternalAuthKey);
        await externalAuthKeyTester.LoginAsAdmin();
        var loginResult = await externalAuthKeyTester.Execute
        (
            new ExternalAuthKeyModel(authenticatorKey, externalUserKey)
        );
        return loginResult;
    }

    private async Task<AppUserModel> GetUser(IHubActionTester tester, AppUserModel user)
    {
        var addUserTester = tester.Create(hubApi => hubApi.UserInquiry.GetUser);
        await addUserTester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var refreshedUser = await addUserTester.Execute
        (
            new AppUserIDRequest(user.ID),
            modifier
        );
        return refreshedUser;
    }

    private static readonly AuthenticatorKey authenticatorKey = new("Auth");

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