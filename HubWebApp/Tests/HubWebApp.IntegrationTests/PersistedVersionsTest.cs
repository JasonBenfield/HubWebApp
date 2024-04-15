using HubWebApp.Extensions;
using Microsoft.Extensions.Hosting;
using XTI_Admin;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_Hub.Abstractions;

namespace HubWebApp.IntegrationTests;

internal sealed class PersistedVersionsTest
{
    [Test]
    public async Task ShouldPersistVersions()
    {
        var sp = setup("Production");
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "versions.json");
        var persistedVersions = new XTI_Hub.PersistedVersions(path);
        var hubAdmin = sp.GetRequiredService<IHubAdministration>();
        var versions = await hubAdmin.Versions(new AppVersionName("ScheduledJobs"), default);
        await persistedVersions.Store(versions);
    }

    [Test]
    public async Task ShouldReadVersions()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "versions.json");
        var reader = new VersionReader(path);
        var versions = await reader.Versions();
        Console.WriteLine(XtiSerializer.Serialize(versions));
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
