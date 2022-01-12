using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Api;
using XTI_App.Hosting;
using XTI_HubAppClient.Extensions;
using XTI_WebApp.Abstractions;

namespace XTI_HubAppClient.ConsoleApp.Extensions;

public static class ConsoleAppExtensions
{
    public static void AddXtiConsoleAppServices(this IServiceCollection services, IHostEnvironment hostEnv, IConfiguration configuration, Action<IServiceProvider, AppAgendaBuilder> build)
    {
        XTI_ConsoleApp.Extensions.ConsoleAppExtensions.AddXtiConsoleAppServices(services, hostEnv, configuration, build);
        services.AddHubClientServices(configuration);
        services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<HubClientAppContext>());
        services.AddScoped<ISourceUserContext>(sp => sp.GetRequiredService<HubClientUserContext>());
    }

    public static void AddAppClientDomainSelector(this IServiceCollection services, Action<IServiceProvider, AppClientDomainSelector> configure)
    {
        HubClientExtensions.AddAppClientDomainSelector(services, configure);
    }
}