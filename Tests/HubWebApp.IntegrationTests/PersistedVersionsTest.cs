using HubWebApp.Extensions;
using Microsoft.Extensions.Hosting;
using XTI_Core.Extensions;

namespace HubWebApp.IntegrationTests;

internal sealed class PersistedVersionsTest
{
    [Test]
    public async Task ShouldPersistVersions()
    {
        var sp = setup("Production");
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "HubWebAppVersions.json");
        var persistedVersions = new XTI_Hub.PersistedVersions(sp.GetRequiredService<AppFactory>(), HubInfo.AppKey, path);
        await persistedVersions.Store();
    }

    private static IServiceProvider setup(string envName = "Development")
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", envName);
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
                    services.AddBasicServicesForHub(hostContext.Configuration, new string[0]);
                }
            )
            .Build();
        var scope = host.Services.CreateScope();
        return scope.ServiceProvider;
    }

}
