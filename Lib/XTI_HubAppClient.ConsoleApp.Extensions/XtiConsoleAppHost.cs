using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Extensions;
using XTI_ConsoleApp.Extensions;
using XTI_Core.Extensions;
using XTI_HubAppClient.Extensions;

namespace XTI_HubAppClient.ConsoleApp.Extensions;

public sealed class XtiConsoleAppHost
{
    public static IHostBuilder CreateDefault(AppKey appKey, string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration
            (
                (hostContext, config) => config.UseXtiConfiguration(hostContext.HostingEnvironment, appKey.Name.DisplayText, appKey.Type.DisplayText, args)
            )
            .ConfigureServices
            (
                (hostContext, services) =>
                {
                    services.AddAppServices();
                    services.AddXtiConsoleAppServices();
                    services.AddHubClientServices();
                    services.AddScoped<SystemUserXtiToken>();
                    services.AddXtiTokenAccessor((sp, tokenAccessor) =>
                    {
                        tokenAccessor.AddToken(() => sp.GetRequiredService<SystemUserXtiToken>());
                        tokenAccessor.UseToken<SystemUserXtiToken>();
                    });
                    services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<HubClientAppContext>());
                    services.AddScoped<ISourceUserContext>(sp => sp.GetRequiredService<HubClientUserContext>());
                }
            );
        return builder;
    }
}
