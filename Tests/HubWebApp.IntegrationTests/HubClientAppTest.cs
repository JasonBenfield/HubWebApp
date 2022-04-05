using Microsoft.Extensions.Hosting;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;
using XTI_Secrets.Extensions;

namespace HubWebApp.IntegrationTests;

internal sealed class HubClientAppTest
{
    [Test]
    public async Task Should()
    {
        var sp = setup();
        var client = sp.GetRequiredService<HubAppClient>();
        var app = client.App;
    }

    private IServiceProvider setup()
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.UseXtiConfiguration(hostingContext.HostingEnvironment, "", "", new string[0]);
            })
            .ConfigureServices
            (
                (hostContext, services) =>
                {
                    services.AddMemoryCache();
                    services.AddSingleton<XtiFolder>();
                    services.AddSingleton(_ => HubInfo.AppKey);
                    services.AddSingleton(_ => AppVersionKey.Current);
                    services.AddFileSecretCredentials();
                    services.AddHubClientServices();
                    services.AddSingleton<SystemUserCredentials>();
                    services.AddSingleton<ISystemUserCredentials>(sp => sp.GetRequiredService<SystemUserCredentials>());
                    services.AddXtiTokenAccessor((sp, tokenAccessor) =>
                    {
                        tokenAccessor.AddToken(() => sp.GetRequiredService<SystemUserXtiToken>());
                        tokenAccessor.UseToken<SystemUserXtiToken>();
                    });
                }
            )
            .Build();
        var scope = host.Services.CreateScope();
        var sp = scope.ServiceProvider;
        return sp;
    }
}