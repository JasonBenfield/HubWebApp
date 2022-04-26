using HubWebApp.Extensions;
using Microsoft.Extensions.Hosting;
using XTI_Core.Extensions;
using XTI_Hub.Abstractions;

namespace HubWebApp.IntegrationTests;

internal sealed class PersistedVersionsTest
{
    [Test]
    public async Task ShouldPersistVersions()
    {
        var sp = setup("Production");
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "HubWebAppVersions.json");
        var persistedVersions = new XTI_Hub.PersistedVersions(path);
        await persistedVersions.Store
        (
            new []
            {
                new XtiVersionModel
                {
                   ID = 1,
                   VersionName = new AppVersionName("Test"),
                   TimeAdded = DateTime.Now,
                   Status = AppVersionStatus.Values.Current,
                   VersionType = AppVersionType.Values.Major,
                   VersionKey = new AppVersionKey(1),
                   VersionNumber = new AppVersionNumber(1,0,0)
                }
            }
        );
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
                    services.AddServicesForHub();
                }
            )
            .Build();
        var scope = host.Services.CreateScope();
        return scope.ServiceProvider;
    }

}
