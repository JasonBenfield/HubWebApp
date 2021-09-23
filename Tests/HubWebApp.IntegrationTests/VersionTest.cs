using HubWebApp.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Configuration.Extensions;
using XTI_Hub;
using XTI_HubAppApi;

namespace HubWebApp.IntegrationTests
{
    sealed class VersionTest
    {
        [Test]
        public async Task ShouldCompleteVersion()
        {
            var services = setup("Production");
            var appFactory = services.GetService<AppFactory>();
            var app = await appFactory.Apps().App(HubInfo.AppKey);
            var version = await app.Version(AppVersionKey.Parse("V1169"));
            await version.Published();
        }

        private static IServiceProvider setup(string envName = "Test")
        {
            Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", envName);
            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration
                (
                    (hostContext, config) =>
                    {
                        config.UseXtiConfiguration(hostContext.HostingEnvironment, new string[] { });
                    }
                )
                .ConfigureServices
                (
                    (hostContext, services) =>
                    {
                        services.AddServicesForHub(hostContext.Configuration);
                    }
                )
                .Build();
            var scope = host.Services.CreateScope();
            return scope.ServiceProvider;
        }

    }
}
