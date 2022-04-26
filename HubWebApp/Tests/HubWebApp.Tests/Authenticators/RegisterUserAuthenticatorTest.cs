using Microsoft.EntityFrameworkCore;
using XTI_Hub.Abstractions;
using XTI_HubAppApi.Authenticators;
using XTI_HubAppApi.UserList;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

internal sealed class RegisterUserAuthenticatorTest
{
    private static readonly AppKey authAppKey =
        new AppKey(new AppName("Auth"), AppType.Values.WebApp);

    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        var user = await addUser(tester, "someone");
        var request = new RegisterUserAuthenticatorRequest
        {
            UserID = user.ID.Value,
            ExternalUserKey = "external.id"
        };
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(request);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        var user = await addUser(tester, "someone");
        var request = new RegisterUserAuthenticatorRequest
        {
            UserID = user.ID.Value,
            ExternalUserKey = "external.id"
        };
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                getFakeAuthModifier(tester),
                HubInfo.Roles.Admin
            );
    }

    [Test]
    public async Task ShouldAddUserAuthenticator()
    {
        var tester = await setup();
        var user = await addUser(tester, "someone");
        var request = new RegisterUserAuthenticatorRequest
        {
            UserID = user.ID.Value,
            ExternalUserKey = "external.id"
        };
        await tester.Execute(request, getFakeAuthModifier(tester).ModKey());
        var app = await getAuthApp(tester);
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var userAuthenticators = await db.UserAuthenticators.Retrieve()
            .ToArrayAsync();
        Assert.That(userAuthenticators.Length, Is.EqualTo(1), "Should add authenticator to user");
        var authenticator = await db.Authenticators.Retrieve()
            .FirstAsync(a => a.AppID == app.ID.Value);
        Assert.That(userAuthenticators[0].AuthenticatorID, Is.EqualTo(authenticator.ID));
        Assert.That(userAuthenticators[0].UserID, Is.EqualTo(user.ID.Value));
        Assert.That(userAuthenticators[0].ExternalUserKey, Is.EqualTo("external.id"));
    }

    [Test]
    public async Task ShouldAddUserAuthenticatorOnlyOnce()
    {
        var tester = await setup();
        var user = await addUser(tester, "someone");
        var request = new RegisterUserAuthenticatorRequest
        {
            UserID = user.ID.Value,
            ExternalUserKey = "external.id"
        };
        var modKey = getFakeAuthModifier(tester).ModKey();
        await tester.Execute(request, modKey);
        await tester.Execute(request, modKey);
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var userAuthenticators = await db.UserAuthenticators.Retrieve().ToArrayAsync();
        Assert.That(userAuthenticators.Length, Is.EqualTo(1), "Should add authenticator to user only once");
    }

    [Test]
    public async Task ShouldUpdateExternalKey_WhenUserAuthenticatorAlreadyExists()
    {
        var tester = await setup();
        var user = await addUser(tester, "someone");
        var request = new RegisterUserAuthenticatorRequest
        {
            UserID = user.ID.Value,
            ExternalUserKey = "external.id"
        };
        var modKey = getFakeAuthModifier(tester).ModKey();
        await tester.Execute(request, modKey);
        request.ExternalUserKey = "external.id2";
        await tester.Execute(request, modKey);
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var userAuthenticators = await db.UserAuthenticators.Retrieve().ToArrayAsync();
        Assert.That
        (
            userAuthenticators[0].ExternalUserKey, 
            Is.EqualTo("external.id2"), 
            "Should update external user key"
        );
    }

    private async Task<HubActionTester<RegisterUserAuthenticatorRequest, EmptyActionResult>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        var tester = HubActionTester.Create
        (
            services,
            hubApi => hubApi.Authenticators.RegisterUserAuthenticator
        );
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var appKey = new AppKey(new AppName("Auth"), AppType.Values.WebApp);
        var authApp = await factory.Apps.AddOrUpdate(new AppVersionName("auth"), appKey, "auth.example.com", DateTimeOffset.Now);
        await authApp.RegisterAsAuthenticator();
        var hubApp = await tester.HubApp();
        var modCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var modifier = await modCategory.AddOrUpdateModifier(authApp.ID.Value, authApp.Key().Name.DisplayText);
        var fakeHubApp = tester.FakeHubApp();
        fakeHubApp.ModCategory(HubInfo.ModCategories.Apps)
            .AddModifier(modifier.ID, modifier.ModKey(), "Auth");
        return tester;
    }

    private FakeModifier getFakeAuthModifier(HubActionTester<RegisterUserAuthenticatorRequest, EmptyActionResult> tester)
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