using Microsoft.EntityFrameworkCore;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

internal sealed class RegisterAuthenticatorTest
{
    private static readonly AppKey authAppKey = 
        new AppKey(new AppName("Auth"), AppType.Values.WebApp);

    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(new EmptyRequest());
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                new EmptyRequest(),
                getFakeAuthModifier(tester),
                HubInfo.Roles.Admin
            );
    }

    [Test]
    public async Task ShouldAddAuthenticator()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var authApp = await getAuthApp(tester);
        await tester.Execute(new EmptyRequest(), getFakeAuthModifier(tester).ModKey());
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var authenticators = await db.Authenticators
            .Retrieve()
            .Join
            (
                db.Apps.Retrieve(),
                auth => auth.AppID,
                app => app.ID,
                (auth, app) => app.Name
            )
            .Select(a => new AppName(a))
            .ToArrayAsync();
        Assert.That
        (
            authenticators,
            Is.EqualTo(new[] { new AppName("Auth") }),
            "Should add authenticator"
        );
    }

    [Test]
    public async Task ShouldAddAuthenticatorOnlyOnce()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var modKey = getFakeAuthModifier(tester).ModKey();
        await tester.Execute(new EmptyRequest(), modKey);
        await tester.Execute(new EmptyRequest(), modKey);
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var authenticators = await db.Authenticators
            .Retrieve()
            .ToArrayAsync();
        Assert.That
        (
            authenticators.Length,
            Is.EqualTo(1),
            "Should add authenticator only once"
        );
    }

    private Task<App> getAuthApp(IHubActionTester tester)
    {
        var factory = tester.Services.GetRequiredService<AppFactory>();
        return factory.Apps.App(authAppKey);
    }

    private async Task<HubActionTester<EmptyRequest, EmptyActionResult>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        var tester = HubActionTester.Create
        (
            services,
            hubApi => hubApi.Authenticators.RegisterAuthenticator
        );
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var authApp = await factory.Apps.AddOrUpdate(new AppVersionName("auth"), authAppKey, "auth.example.com", DateTimeOffset.Now);
        var appKey = new AppKey(new AppName("Auth"), AppType.Values.WebApp);
        var hubApp = await tester.HubApp();
        var modCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var modifier = await modCategory.AddOrUpdateModifier(authApp.ID.Value, authApp.Key().Name.DisplayText);
        var fakeHubApp = tester.FakeHubApp();
        fakeHubApp.ModCategory(HubInfo.ModCategories.Apps)
            .AddModifier(modifier.ID, modifier.ModKey(), "Auth");
        return tester;
    }

    private FakeModifier getFakeAuthModifier(HubActionTester<EmptyRequest, EmptyActionResult> tester)
    {
        return tester.FakeHubApp()
            .ModCategory(HubInfo.ModCategories.Apps)
            .ModifierByTargetID("Auth");
    }

}