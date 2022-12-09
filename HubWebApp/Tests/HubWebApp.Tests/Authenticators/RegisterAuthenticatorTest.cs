using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

internal sealed class RegisterAuthenticatorTest
{
    private static readonly AuthenticatorKey authenticatorKey = new AuthenticatorKey("Auth");

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                new RegisterAuthenticatorRequest(authenticatorKey),
                HubInfo.Roles.Admin
            );
    }

    [Test]
    public async Task ShouldAddAuthenticator()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        await tester.Execute(new RegisterAuthenticatorRequest(authenticatorKey));
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var authenticators = await db.Authenticators
            .Retrieve()
            .ToArrayAsync();
        Assert.That
        (
            authenticators.Select(a => a.AuthenticatorKey),
            Is.EqualTo(new[] { authenticatorKey.Value }),
            "Should add authenticator"
        );
        Assert.That
        (
            authenticators.Select(a => a.AuthenticatorName),
            Is.EqualTo(new[] { authenticatorKey.DisplayText }),
            "Should add authenticator"
        );
    }

    [Test]
    public async Task ShouldAddAuthenticatorOnlyOnce()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        await tester.Execute(new RegisterAuthenticatorRequest(authenticatorKey));
        await tester.Execute(new RegisterAuthenticatorRequest(authenticatorKey));
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

    private async Task<HubActionTester<RegisterAuthenticatorRequest, AuthenticatorModel>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        var tester = HubActionTester.Create
        (
            services,
            hubApi => hubApi.Authenticators.RegisterAuthenticator
        );
        return tester;
    }
}