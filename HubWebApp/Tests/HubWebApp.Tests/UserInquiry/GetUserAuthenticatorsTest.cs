namespace HubWebApp.Tests;

internal sealed class GetUserAuthenticatorsTest
{
    private static readonly AppKey authAppKey = AppKey.WebApp("Auth");

    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var user = await AddUser(tester, "User1");
        await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(user.ID);
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
                user.ID,
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
        var userAuthenticators = await tester.Execute(user.ID, modifier);
        Assert.That
        (
            userAuthenticators,
            Is.EquivalentTo(new[] { new UserAuthenticatorModel(authAppKey, externalUserKey) })
        );
    }

    private async Task<HubActionTester<int, UserAuthenticatorModel[]>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        var tester =  HubActionTester.Create(sp, hubApi => hubApi.UserInquiry.GetUserAuthenticators);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var appKey = AppKey.WebApp("Auth");
        var authApp = await factory.Apps.AddOrUpdate(new AppVersionName("auth"), appKey, DateTimeOffset.Now);
        await authApp.RegisterAsAuthenticator();
        var appRegistration = tester.Services.GetRequiredService<AppRegistration>();
        await appRegistration.Run
        (
            new AppApiTemplateModel
            {
                AppKey = appKey,
                GroupTemplates = new AppApiGroupTemplateModel[0]
            },
            AppVersionKey.Current
        );
        var hubApp = await tester.HubApp();
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

    private Task RegisterUserAuthenticator(IHubActionTester tester, int userID, string externalUserKey)
    {
        var registerTester = tester.Create(api => api.Authenticators.RegisterUserAuthenticator);
        return registerTester.Execute
        (
            new RegisterUserAuthenticatorRequest
            {
                UserID = userID,
                ExternalUserKey = externalUserKey
            },
            new ModifierKey(authAppKey.Format())
        );
    }

    private Task<App> GetAuthApp(IHubActionTester tester)
    {
        var factory = tester.Services.GetRequiredService<HubFactory>();
        return factory.Apps.App(authAppKey);
    }
}
