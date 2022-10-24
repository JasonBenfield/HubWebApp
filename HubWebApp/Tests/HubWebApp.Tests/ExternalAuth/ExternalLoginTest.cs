using HubWebApp.Fakes;
using System.Security.Claims;
using XTI_Hub.Abstractions;
using XTI_HubWebAppApi.Auth;
using XTI_HubWebAppApi.ExternalAuth;
using XTI_HubWebAppApi.UserList;
using XTI_WebApp.Api;

namespace HubWebApp.Tests;

internal sealed class ExternalLoginTest
{
    private static readonly AppKey authAppKey = AppKey.WebApp("Auth");

    [Test]
    public async Task ShouldAuthenticateUser()
    {
        var tester = await setup();
        var user = await addUser(tester, "someone");
        var authApp = await getAuthApp(tester);
        var authAppModel = authApp.ToModel();
        const string externalUserKey = "external.id";
        await user.AddAuthenticator(authApp, externalUserKey);
        var authKey = await externalAuthKey(tester, authAppModel.AppKey, externalUserKey);
        var returnKey = await loginReturnKey(tester, "/Home");
        var request = new LoginModel
        {
            AuthKey = authKey,
            ReturnKey = returnKey
        };
        await tester.Execute(request, authAppModel.PublicKey);
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

    private async Task<HubActionTester<LoginModel, WebRedirectResult>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        var tester = HubActionTester.Create
        (
            services,
            hubApi => hubApi.Auth.Login
        );
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var appKey = AppKey.WebApp("Auth");
        var authApp = await factory.Apps.AddOrUpdate(new AppVersionName("auth"), appKey, DateTimeOffset.Now);
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
        await authApp.RegisterAsAuthenticator();
        return tester;
    }

    private Task<App> getAuthApp(IHubActionTester tester)
    {
        var factory = tester.Services.GetRequiredService<HubFactory>();
        return factory.Apps.App(authAppKey);
    }

    private async Task<AppUser> addUser(IHubActionTester tester, string userName)
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

    private async Task<string> externalAuthKey(IHubActionTester tester, AppKey appKey, string externalUserKey)
    {
        var externalAuthKeyTester = tester.Create(hubApi => hubApi.ExternalAuth.ExternalAuthKey);
        await externalAuthKeyTester.LoginAsAdmin();
        var authKey = await externalAuthKeyTester.Execute
        (
            new ExternalAuthKeyModel
            {
                ExternalUserKey = externalUserKey
            },
            new ModifierKey(appKey.Format())
        );
        return authKey;
    }

    private async Task<string> loginReturnKey(IHubActionTester tester, string returnUrl)
    {
        var loginReturnKeyTester = tester.Create(hubApi => hubApi.Auth.LoginReturnKey);
        await loginReturnKeyTester.LoginAsAdmin();
        var result = await loginReturnKeyTester.Execute(new LoginReturnModel
        {
            ReturnUrl = returnUrl
        });
        return result;
    }
}