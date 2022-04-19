using HubWebApp.Fakes;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using XTI_HubAppApi.ExternalAuth;
using XTI_HubAppApi.UserList;
using XTI_HubDB.Entities;
using XTI_WebApp.Api;

namespace HubWebApp.Tests;

internal sealed class ExternalLoginTest
{
    private static readonly AppKey authAppKey =
        new AppKey(new AppName("Auth"), AppType.Values.WebApp);

    [Test]
    public async Task ShouldAuthenticateUser()
    {
        var tester = await setup();
        var user = await addUser(tester, "someone");
        var authApp = await getAuthApp(tester);
        await user.AddAuthenticator(authApp, "external.id");
        var request = new ExternalLoginRequest
        {
            ExternalUserKey = "external.id"
        };
        await tester.Execute(request, getFakeAuthModifier(tester).ModKey());
        var access = tester.Services.GetRequiredService<FakeAccessForLogin>();
        Assert.That
        (
            access.Claims,
            Has.One.EqualTo
            (
                new Claim("UserName", user.UserName().Value)
            )
            .Using<Claim>((x, y) => x.Type == y.Type && x.Value == y.Value),
            "Should authenticate user"
        );
    }

    private async Task<HubActionTester<ExternalLoginRequest, WebRedirectResult>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        var tester = HubActionTester.Create
        (
            services,
            hubApi => hubApi.ExternalAuth.Login
        );
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var appKey = new AppKey(new AppName("Auth"), AppType.Values.WebApp);
        var authApp = await factory.Apps.AddOrUpdate(appKey, "auth.example.com", DateTimeOffset.Now);
        await authApp.RegisterAsAuthenticator();
        var hubApp = await tester.HubApp();
        var modCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var modifier = await modCategory.AddOrUpdateModifier(authApp.ID.Value, authApp.Key().Name.DisplayText);
        var fakeHubApp = tester.FakeHubApp();
        fakeHubApp.ModCategory(HubInfo.ModCategories.Apps)
            .AddModifier(modifier.ID, modifier.ModKey(), "Auth");
        return tester;
    }
    private FakeModifier getFakeAuthModifier(HubActionTester<ExternalLoginRequest, WebRedirectResult> tester)
    {
        return tester.FakeHubApp()
            .ModCategory(HubInfo.ModCategories.Apps)
            .ModifierByTargetID("Auth");
    }

    private Task<App> getAuthApp(IHubActionTester tester)
    {
        var factory = tester.Services.GetRequiredService<AppFactory>();
        return factory.Apps.App(authAppKey);
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