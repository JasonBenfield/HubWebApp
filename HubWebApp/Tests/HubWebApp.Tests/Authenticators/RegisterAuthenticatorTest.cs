using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

internal sealed class RegisterAuthenticatorTest
{
    private static readonly AppKey authAppKey = AppKey.WebApp("Auth");

    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(new EmptyRequest());
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        var modifier = await getFakeAuthModifier(tester);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                new EmptyRequest(),
                modifier,
                HubInfo.Roles.Admin
            );
    }

    [Test]
    public async Task ShouldAddAuthenticator()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var authApp = await getAuthApp(tester);
        var modifier = await getFakeAuthModifier(tester);
        await tester.Execute(new EmptyRequest(), modifier);
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
        await tester.LoginAsAdmin();
        var modifier = await getFakeAuthModifier(tester);
        await tester.Execute(new EmptyRequest(), modifier);
        await tester.Execute(new EmptyRequest(), modifier);
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
        var factory = tester.Services.GetRequiredService<HubFactory>();
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
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var authApp = await factory.Apps.AddOrUpdate(new AppVersionName("auth"), authAppKey, DateTimeOffset.Now);
        var appRegistration = tester.Services.GetRequiredService<AppRegistration>();
        await appRegistration.Run
        (
            new AppApiTemplateModel
            {
                AppKey = authAppKey,
                GroupTemplates = new AppApiGroupTemplateModel[0]
            },
            AppVersionKey.Current
        );
        return tester;
    }

    private async Task<Modifier> getFakeAuthModifier(IHubActionTester tester)
    {
        var hubApp = await tester.HubApp();
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var authApp = await factory.Apps.App(authAppKey);
        var modifier = await appsModCategory.ModifierByModKey(authApp.ToModel().PublicKey);
        return modifier;
    }

}