using HubWebApp.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_Core;
using XTI_Core.Fakes;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_TempLog;
using XTI_WebApp.Api;
using XTI_WebApp.Fakes;

namespace HubWebApp.Tests;

internal sealed class LogoutTest
{
    [Test]
    public async Task ShouldEndSession()
    {
        var services = await setup();
        await execute(services);
        var tempLog = services.GetRequiredService<TempLog>();
        var clock = services.GetRequiredService<IClock>();
        var endSessionFiles = tempLog.EndSessionFiles(clock.Now().AddMinutes(1)).ToArray();
        Assert.That
        (
            endSessionFiles.Length,
            Is.EqualTo(1),
            "Should end session"
        );
    }

    [Test]
    public async Task ShouldRedirectToLogin()
    {
        var services = await setup();
        var result = await execute(services);
        Assert.That
        (
            result.Data.Url,
            Is.EqualTo("/Hub/Current/Auth"),
            "Should redirect to login"
        );
    }

    private async Task<IServiceProvider> setup()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices
            (
                (hostContext, services) =>
                {
                    services.AddFakesForXtiWebApp(hostContext.Configuration);
                    services.AddFakesForHubWebApp(hostContext.Configuration);
                }
            )
            .Build();
        var scope = host.Services.CreateScope();
        var sp = scope.ServiceProvider;
        var hubSetup = sp.GetRequiredService<IAppSetup>();
        await hubSetup.Run(AppVersionKey.Current);
        var api = sp.GetRequiredService<HubAppApi>();
        var appFactory = sp.GetRequiredService<AppFactory>();
        await appFactory.Users.Add
        (
            new AppUserName("test.user"),
            new FakeHashedPassword("Password12345"),
            DateTime.UtcNow
        );
        var tempLogSession = sp.GetRequiredService<TempLogSession>();
        await tempLogSession.StartSession();
        return sp;
    }

    private static Task<ResultContainer<WebRedirectResult>> execute(IServiceProvider services)
    {
        var api = services.GetRequiredService<HubAppApi>();
        return api.Auth.Logout.Execute(new EmptyRequest());
    }
}