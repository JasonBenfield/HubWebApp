using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_App.Secrets;
using XTI_Configuration.Extensions;
using XTI_Core;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;
using XTI_Secrets.Extensions;
using XTI_WebAppClient;

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
                config.UseXtiConfiguration(hostingContext.HostingEnvironment, new string[0]);
            })
            .ConfigureServices
            (
                (hostContext, services) =>
                {
                    services.AddMemoryCache();
                    services.AddSingleton<XtiFolder>();
                    services.AddSingleton(_ => HubInfo.AppKey);
                    services.AddFileSecretCredentials(hostContext.HostingEnvironment);
                    services.AddSingleton<InstallationUserCredentials>();
                    services.AddSingleton<IInstallationUserCredentials>(sp => sp.GetRequiredService<InstallationUserCredentials>());
                    services.AddSingleton<SystemUserCredentials>();
                    services.AddSingleton<ISystemUserCredentials>(sp => sp.GetRequiredService<SystemUserCredentials>());
                    services.AddHubClientServices(hostContext.Configuration);
                    services.AddScoped(sp =>
                    {
                        var credentials = sp.GetRequiredService<IInstallationUserCredentials>();
                        return new XtiTokenFactory(credentials);
                    });
                }
            )
            .Build();
        var scope = host.Services.CreateScope();
        var sp = scope.ServiceProvider;
        return sp;
    }
}