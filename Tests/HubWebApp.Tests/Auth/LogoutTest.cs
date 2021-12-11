using HubWebApp.Fakes;
using XTI_HubAppApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_Core;
using XTI_Core.Fakes;
using XTI_TempLog;
using XTI_WebApp.Api;
using XTI_WebApp.Fakes;

namespace HubWebApp.Tests
{
    public class LogoutTest
    {
        [Test]
        public async Task ShouldEndSession()
        {
            var services = await setup();
            await execute(services);
            var tempLog = services.GetService<TempLog>();
            var clock = services.GetService<Clock>();
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
            var api = sp.GetService<HubAppApi>();
            var input = new TestInput(sp, api);
            await input.AppFactory.Users.Add
            (
                new AppUserName("test.user"),
                new FakeHashedPassword("Password12345"),
                DateTime.UtcNow
            );
            var tempLogSession = sp.GetService<TempLogSession>();
            await tempLogSession.StartSession();
            return sp;
        }

        private static Task<ResultContainer<WebRedirectResult>> execute(IServiceProvider services)
        {
            var api = services.GetService<HubAppApi>();
            return api.Auth.Logout.Execute(new EmptyRequest());
        }

        private class TestInput
        {
            public TestInput(IServiceProvider sp, HubAppApi api)
            {
                Api = api;
                AppFactory = sp.GetService<AppFactory>();
                Clock = (FakeClock)sp.GetService<Clock>();
                TempLog = sp.GetService<TempLog>();
            }
            public HubAppApi Api { get; }
            public AppFactory AppFactory { get; }
            public FakeClock Clock { get; }
            public TempLog TempLog { get; }
        }
    }
}