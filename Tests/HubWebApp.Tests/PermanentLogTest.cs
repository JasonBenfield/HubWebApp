using HubWebApp.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_AppSetupApp.Extensions;
using XTI_Core;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.PermanentLog;
using XTI_TempLog;
using XTI_TempLog.Fakes;

namespace HubWebApp.Tests
{
    public sealed class PermanentLogTest
    {
        [Test]
        public async Task ShouldStartSessionOnPermanentLog()
        {
            var services = await setup();
            var sessionKey = generateKey();
            await startSession(services, sessionKey);
            var factory = services.GetService<AppFactory>();
            var session = await factory.Sessions().Session(sessionKey);
            Assert.That(session.HasStarted(), Is.True, "Should start session on permanent log");
            Assert.That(session.HasEnded(), Is.False, "Should start session on permanent log");
        }

        [Test]
        public async Task ShouldStartRequestOnPermanentLog()
        {
            var services = await setup();
            var sessionKey = generateKey();
            await startSession(services, sessionKey);
            var requestKey = generateKey();
            await startRequest(services, sessionKey, requestKey);
            var factory = services.GetService<AppFactory>();
            var session = await factory.Sessions().Session(sessionKey);
            var requests = (await session.Requests()).ToArray();
            Assert.That(requests.Length, Is.EqualTo(1), "Should start request on permanent log");
        }

        [Test]
        public async Task ShouldEndRequestOnPermanentLog()
        {
            var services = await setup();
            var sessionKey = generateKey();
            await startSession(services, sessionKey);
            var requestKey = generateKey();
            await startRequest(services, sessionKey, requestKey);
            await endRequest(services, requestKey);
            var factory = services.GetService<AppFactory>();
            var session = await factory.Sessions().Session(sessionKey);
            var requests = (await session.Requests()).ToArray();
            Assert.That(requests[0].HasEnded(), Is.True, "Should end request on permanent log");
        }

        [Test]
        public async Task ShouldEndSessionOnPermanentLog()
        {
            var services = await setup();
            var sessionKey = generateKey();
            await startSession(services, sessionKey);
            var requestKey = generateKey();
            await startRequest(services, sessionKey, requestKey);
            await endRequest(services, requestKey);
            await endSession(services, sessionKey);
            var factory = services.GetService<AppFactory>();
            var session = await factory.Sessions().Session(sessionKey);
            Assert.That(session.HasEnded(), Is.True, "Should end session on permanent log");
        }

        [Test]
        public async Task ShouldAuthenticateSessionOnPermanentLog()
        {
            var services = await setup();
            var sessionKey = generateKey();
            await startSession(services, sessionKey);
            await authenticateSession(services, sessionKey);
            var factory = services.GetService<AppFactory>();
            var session = await factory.Sessions().Session(sessionKey);
            var user = await factory.Users().User(session.UserID);
            Assert.That(user.UserName, Is.EqualTo("someone"), "Should authenticate session on permanent log");
        }

        [Test]
        public async Task ShouldLogEventOnPermanentLog()
        {
            var services = await setup();
            var sessionKey = generateKey();
            await startSession(services, sessionKey);
            var requestKey = generateKey();
            await startRequest(services, sessionKey, requestKey);
            Exception exception;
            try
            {
                throw new Exception("Test");
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            await logEvent(services, requestKey, exception);
            var factory = services.GetService<AppFactory>();
            var session = await factory.Sessions().Session(sessionKey);
            var requests = (await session.Requests()).ToArray();
            var events = (await requests[0].Events()).ToArray();
            Assert.That(events.Length, Is.EqualTo(1), "Should log event on permanent log");
        }

        private static Task startSession(IServiceProvider services, string sessionKey)
        {
            var clock = services.GetService<Clock>();
            var startSessionModel = new StartSessionModel
            {
                SessionKey = sessionKey,
                RemoteAddress = "my-computer",
                UserAgent = "Windows 10",
                TimeStarted = clock.Now(),
                UserName = "someone"
            };
            var api = services.GetService<HubAppApi>();
            return api.PermanentLog.StartSession.Execute(startSessionModel);
        }

        private static async Task startRequest(IServiceProvider services, string sessionKey, string requestKey)
        {
            var clock = services.GetService<Clock>();
            var startRequestModel = new StartRequestModel
            {
                RequestKey = requestKey,
                SessionKey = sessionKey,
                TimeStarted = clock.Now(),
                AppType = AppType.Values.WebApp.DisplayText,
                Path = "/Fake/Current/Test/Action1"
            };
            var api = services.GetService<HubAppApi>();
            await api.PermanentLog.StartRequest.Execute(startRequestModel);
        }

        private static Task endRequest(IServiceProvider services, string requestKey)
        {
            var clock = services.GetService<Clock>();
            var api = services.GetService<HubAppApi>();
            return api.PermanentLog.EndRequest.Execute
            (
                new EndRequestModel
                {
                    RequestKey = requestKey,
                    TimeEnded = clock.Now()
                }
            );
        }

        private static Task endSession(IServiceProvider services, string sessionKey)
        {
            var clock = services.GetService<Clock>();
            var api = services.GetService<HubAppApi>();
            return api.PermanentLog.EndSession.Execute
            (
                new EndSessionModel
                {
                    SessionKey = sessionKey,
                    TimeEnded = clock.Now()
                }
            );
        }

        private static Task authenticateSession(IServiceProvider services, string sessionKey)
        {
            var api = services.GetService<HubAppApi>();
            return api.PermanentLog.AuthenticateSession.Execute
            (
                new AuthenticateSessionModel
                {
                    SessionKey = sessionKey,
                    UserName = "someone"
                }
            );
        }

        private static Task logEvent(IServiceProvider services, string requestKey, Exception exception)
        {
            var api = services.GetService<HubAppApi>();
            return api.PermanentLog.LogEvent.Execute
            (
                new LogEventModel
                {
                    EventKey = generateKey(),
                    RequestKey = requestKey,
                    Severity = AppEventSeverity.Values.CriticalError,
                    Caption = "An unexpected error occurred",
                    Message = exception.Message,
                    Detail = exception.StackTrace
                }
            );
        }

        private static string generateKey() => Guid.NewGuid().ToString("N");

        private async Task<IServiceProvider> setup()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices
                (
                    (hostContext, services) =>
                    {
                        services.AddFakeTempLogServices();
                        services.AddFakesForHubWebApp(hostContext.Configuration);
                        services.AddScoped<IAppApiUser, AppApiSuperUser>();
                        services.AddSingleton<IAppEnvironmentContext, FakeAppEnvironmentContext>();
                        services.AddSingleton(sp => HubInfo.AppKey);
                        services.AddScoped<PermanentLog>();
                        services.AddScoped<HubAppApi>();
                    }
                )
                .Build();
            var scope = host.Services.CreateScope();
            var sp = scope.ServiceProvider;
            var appEnvContext = (FakeAppEnvironmentContext)sp.GetService<IAppEnvironmentContext>();
            appEnvContext.Environment = new AppEnvironment
            (
                "test.user",
                "Requester",
                "my-computer",
                "Windows 10",
                AppType.Values.WebApp.DisplayText
            );
            var appFactory = sp.GetService<AppFactory>();
            var clock = sp.GetService<Clock>();
            var hubSetup = sp.GetService<HubAppSetup>();
            await hubSetup.Run(AppVersionKey.Current);
            var app = await appFactory.Apps().Add(new AppKey(new AppName("Fake"), AppType.Values.WebApp), "Fake", DateTime.Now);
            var version = await app.StartNewMajorVersion(DateTime.Now);
            await version.Publishing();
            await version.Published();
            await appFactory.Users().Add(new AppUserName("test.user"), new FakeHashedPassword("Password12345"), DateTime.Now);
            await appFactory.Users().Add(new AppUserName("Someone"), new FakeHashedPassword("Password12345"), DateTime.Now);
            return sp;
        }
    }
}
