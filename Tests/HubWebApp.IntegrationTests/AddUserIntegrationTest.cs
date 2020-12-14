using HubWebApp.Api;
using HubWebApp.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_Core;

namespace HubWebApp.IntegrationTests
{
    public sealed class AddUserIntegrationTest
    {
        [Test]
        public async Task ShouldAddUser()
        {
            Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Test");
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices
                (
                    (hostContext, services) =>
                    {
                        services.AddServicesForHub(hostContext.Configuration);
                    }
                )
                .Build();
            var scope = host.Services.CreateScope();
            var sp = scope.ServiceProvider;
            var factory = sp.GetService<AppFactory>();
            var clock = sp.GetService<Clock>();
            var setup = new AllAppSetup(factory, clock);
            await setup.Run();
            var anonUser = await factory.Users().User(AppUserName.Anon);
            Assert.That(anonUser.Exists(), Is.True, "Should add anonymous user");
        }
    }
}
