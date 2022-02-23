using HubWebApp.Extensions;
using Microsoft.Extensions.Hosting;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_HubDB.EF;

namespace HubWebApp.IntegrationTests;

public sealed class AppSessionTest
{
    [Test]
    public async Task ShouldGetActiveSessions()
    {
        var services = await setup();
        var factory = services.GetRequiredService<AppFactory>();
        var user = await factory.Users.Anon();
        var createdSession = await factory.Sessions.AddOrUpdate
        (
            "JustCreated",
            user,
            DateTimeOffset.UtcNow,
            "Testing",
            "UserAgent",
            "127.0.0.1"
        );
        var activeSessions = (await factory.Sessions.ActiveSessions(DateTimeRange.OnOrBefore(DateTime.UtcNow.AddDays(1)))).ToArray();
        Assert.That(activeSessions.Length, Is.EqualTo(1), "Should include the session that was just created");
        Assert.That(activeSessions[0].HasStarted(), Is.True);
        Assert.That(activeSessions[0].HasEnded(), Is.False);
        await createdSession.End(DateTimeOffset.UtcNow);
        activeSessions = (await factory.Sessions.ActiveSessions(DateTimeRange.OnOrBefore(DateTime.UtcNow.AddDays(1)))).ToArray();
        Assert.That(activeSessions.Length, Is.EqualTo(0), "Should not include session after it ended");
    }

    [Test]
    public async Task ShouldGetMostRecentRequest()
    {
        var services = await setup();
        var factory = services.GetRequiredService<AppFactory>();
        var user = await factory.Users.Anon();
        var createdSession = await createSession(factory, user);
        await logRequest(services, createdSession);
        var activeSessions = (await factory.Sessions.ActiveSessions(DateTimeRange.OnOrBefore(DateTime.UtcNow.AddDays(1)))).ToArray();
        var requests = (await activeSessions[0].MostRecentRequests(1)).ToArray();
        Assert.That(requests.Length, Is.EqualTo(1), "Should get most recent request");
    }

    [Test]
    public async Task ShouldGetMostRecentRequestsForApp()
    {
        var services = await setup();
        var factory = services.GetRequiredService<AppFactory>();
        var user = await factory.Users.Anon();
        var createdSession = await createSession(factory, user);
        await logRequest(services, createdSession);
        var app = await factory.Apps.App(HubInfo.AppKey);
        var requests = (await app.MostRecentRequests(10)).ToArray();
        Assert.That(requests.Length, Is.EqualTo(1));
    }

    [Test]
    public async Task ShouldGetMostRecentErrorEventsForApp()
    {
        var services = await setup();
        var factory = services.GetRequiredService<AppFactory>();
        var user = await factory.Users.Anon();
        var createdSession = await createSession(factory, user);
        var request = await logRequest(services, createdSession);
        await logEvent(request);
        var app = await factory.Apps.App(HubInfo.AppKey);
        var events = (await app.MostRecentErrorEvents(10)).ToArray();
        Assert.That(events.Length, Is.EqualTo(1));
    }

    private static async Task<IServiceProvider> setup()
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Test");
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration
            (
                (hostContext, config) =>
                {
                    config.UseXtiConfiguration(hostContext.HostingEnvironment, "", "", new string[0]);
                }
            )
            .ConfigureServices
            (
                (hostContext, services) =>
                {
                    services.AddServicesForHub(hostContext.Configuration, new string[0]);
                }
            )
            .Build();
        var scope = host.Services.CreateScope();
        var resetDB = scope.ServiceProvider.GetRequiredService<HubDbReset>();
        await resetDB.Run();
        var setup = scope.ServiceProvider.GetRequiredService<IAppSetup>();
        await setup.Run(AppVersionKey.Current);
        return scope.ServiceProvider;
    }

    private static Task<AppSession> createSession(AppFactory factory, AppUser user)
    {
        return factory.Sessions.AddOrUpdate
        (
            "JustCreated",
            user,
            DateTimeOffset.UtcNow,
            "Testing",
            "UserAgent",
            "127.0.0.1"
        );
    }

    private static async Task<AppRequest> logRequest(IServiceProvider services, AppSession createdSession)
    {
        var factory = services.GetRequiredService<AppFactory>();
        var app = await factory.Apps.App(HubInfo.AppKey);
        var version = await app.CurrentVersion();
        var resourceGroup = await version.ResourceGroupByName(new ResourceGroupName("Employee"));
        var resource = await resourceGroup.ResourceByName(new ResourceName("AddEmployee"));
        var modifier = await app.DefaultModifier();
        var request = await createdSession.LogRequest
        (
            "New-Request",
            resource,
            modifier,
            "/Hub/Current",
            DateTimeOffset.UtcNow
        );
        return request;
    }

    private static Task<AppEvent> logEvent(AppRequest request)
    {
        return request.LogEvent
        (
            new GeneratedKey().Value(),
            AppEventSeverity.Values.CriticalError,
            DateTimeOffset.Now,
            "Test",
            "Test Error",
            "Testing"
        );
    }
}