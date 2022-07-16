using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;
using XTI_HubWebAppApi.Authenticators;
using XTI_HubWebAppApi.UserList;

namespace HubWebApp.Tests;

internal sealed class RegisterUserAuthenticatorTest
{
    private static readonly AppKey authAppKey = AppKey.WebApp("Auth");

    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        var user = await addUser(tester, "someone");
        var request = new RegisterUserAuthenticatorRequest
        {
            UserID = user.ToModel().ID,
            ExternalUserKey = "external.id"
        };
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(request);
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        var user = await addUser(tester, "someone");
        var request = new RegisterUserAuthenticatorRequest
        {
            UserID = user.ToModel().ID,
            ExternalUserKey = "external.id"
        };
        var modifier = await tester.AppModifier(authAppKey);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                modifier,
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
            UserID = user.ToModel().ID,
            ExternalUserKey = "external.id"
        };
        var modKey = new ModifierKey(authAppKey.Format());
        await tester.Execute(request, modKey);
        var app = await getAuthApp(tester);
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var userAuthenticators = await db.UserAuthenticators.Retrieve()
            .ToArrayAsync();
        Assert.That(userAuthenticators.Length, Is.EqualTo(1), "Should add authenticator to user");
        var authenticator = await db.Authenticators.Retrieve()
            .FirstAsync(a => a.AppID == app.ID);
        Assert.That(userAuthenticators[0].AuthenticatorID, Is.EqualTo(authenticator.ID));
        Assert.That(userAuthenticators[0].UserID, Is.EqualTo(user.ToModel().ID));
        Assert.That(userAuthenticators[0].ExternalUserKey, Is.EqualTo("external.id"));
    }

    [Test]
    public async Task ShouldAddUserAuthenticatorOnlyOnce()
    {
        var tester = await setup();
        var user = await addUser(tester, "someone");
        var request = new RegisterUserAuthenticatorRequest
        {
            UserID = user.ToModel().ID,
            ExternalUserKey = "external.id"
        };
        var modKey = new ModifierKey(authAppKey.Format());
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
            UserID = user.ToModel().ID,
            ExternalUserKey = "external.id"
        };
        var modifier = await getFakeAuthModifier(tester);
        await tester.Execute(request, modifier);
        request.ExternalUserKey = "external.id2";
        await tester.Execute(request, modifier);
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
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var appKey = AppKey.WebApp("Auth");
        var authApp = await factory.Apps.AddOrUpdate(new AppVersionName("auth"), appKey, DateTimeOffset.Now);
        await authApp.RegisterAsAuthenticator();
        var hubApp = await tester.HubApp();
        var modCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var authAppModel = authApp.ToAppModel();
        var modifier = await modCategory.AddOrUpdateModifier
        (
            authAppModel.PublicKey,
            authAppModel.ID, 
            authAppModel.AppKey.Format()
        );
        return tester;
    }

    private async Task<Modifier> getFakeAuthModifier(IHubActionTester tester)
    {
        var hubApp = await tester.HubApp();
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var authApp = await factory.Apps.App(authAppKey);
        var modifier = await appsModCategory.ModifierByModKey(authApp.ToAppModel().PublicKey);
        return modifier;
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
        var userID = await addUserTester.Execute(new AddUserModel
        {
            UserName = userName,
            Password = "Password12345"
        });
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var user = await factory.Users.UserByUserName(new AppUserName(userName));
        return user;
    }
}