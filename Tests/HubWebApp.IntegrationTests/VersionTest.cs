using HubWebApp.Extensions;
using Microsoft.Extensions.Hosting;
using XTI_Core.Extensions;

namespace HubWebApp.IntegrationTests;

sealed class VersionTest
{
    [Test]
    public async Task ShouldCompleteVersion()
    {
        var services = setup("Development");
        var appFactory = services.GetRequiredService<AppFactory>();
        var app = await appFactory.Apps.App(HubInfo.AppKey);
        var roles = await app.Roles();
        var denyAccessRole = roles.FirstOrDefault(r => r.Name().Equals(AppRoleName.DenyAccess));
        var version = await app.Version(AppVersionKey.Parse("V1169"));
        await version.Published();
    }

    private static IServiceProvider setup(string envName = "Test")
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", envName);
        var appKey = HubInfo.AppKey;
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration
            (
                (hostContext, config) =>
                {
                    config.UseXtiConfiguration(hostContext.HostingEnvironment, appKey.Name.DisplayText, appKey.Type.DisplayText, new string[0]);
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