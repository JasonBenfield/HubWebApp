using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Secrets;
using XTI_Core.Extensions;
using XTI_TempLog.Abstractions;
using XTI_WebApp.Abstractions;
using XTI_WebAppClient;

namespace XTI_HubAppClient.Extensions;

public static class HubClientExtensions
{
    public static void AddHubClientServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddConfigurationOptions<HubClientOptions>(HubClientOptions.HubClient);
        services.AddScoped<AnonymousXtiToken>();
        services.AddXtiTokenAccessorFactory((sp, accessor) => { });
        services.AddScoped<HubClientOptionsAppClientDomain>();
        services.AddScoped<HubClientAppClientDomain>();
        if (!services.Any(s => s.ImplementationType == typeof(AppClientDomainSelector)))
        {
            AddAppClientDomainSelector(services, (sp, domains) => { });
        }
        services.AddScoped<IAppClientDomain>(sp => sp.GetRequiredService<AppClientDomainSelector>());
        services.AddScoped<AppClientUrl>();
        services.AddSingleton<HubAppClientVersion>();
        services.AddScoped<HubAppClientFactory>();
        services.AddScoped(sp => sp.GetRequiredService<HubAppClientFactory>().Create());
        services.AddScoped<SystemHubAppClient>();
        services.AddScoped<IAuthClient>(sp => sp.GetRequiredService<HubAppClient>());
        services.AddScoped<IPermanentLogClient, PermanentLogClient>();
    }

    public static void AddHubClientContext(this IServiceCollection services)
    {
        services.AddScoped<HubAppClientContext>();
        services.AddScoped(sp => sp.GetRequiredService<HubAppClientContext>().AppContext);
        services.AddScoped(sp => sp.GetRequiredService<HubAppClientContext>().UserContext);
    }

    public static void AddInstallationUserXtiToken(this IServiceCollection services)
    {
        services.AddScoped<InstallationUserCredentials>();
        services.AddScoped<IInstallationUserCredentials>(sp => sp.GetRequiredService<InstallationUserCredentials>());
        services.AddScoped<InstallationUserXtiToken>();
    }

    public static void AddSystemUserXtiToken(this IServiceCollection services)
    {
        services.AddScoped<SystemUserCredentials>();
        services.AddScoped<ISystemUserCredentials>(sp => sp.GetRequiredService<SystemUserCredentials>());
        services.AddScoped<SystemUserXtiToken>();
    }

    public static void AddConfigurationXtiToken(this IServiceCollection services)
    {
        services.AddConfigurationOptions<XtiTokenOptions>(XtiTokenOptions.XtiToken);
        services.AddScoped<ConfigurationXtiToken>();
    }

    public static void AddAppClientDomainSelector(this IServiceCollection services, Action<IServiceProvider, AppClientDomainSelector> configure)
    {
        var existing = services.FirstOrDefault(s => s.ImplementationType == typeof(AppClientDomainSelector));
        if (existing != null)
        {
            services.Remove(existing);
        }
        services.AddScoped
        (
            sp =>
            {
                var appClientDomains = new AppClientDomainSelector();
                appClientDomains.AddAppClientDomain(() => sp.GetRequiredService<HubClientOptionsAppClientDomain>());
                configure(sp, appClientDomains);
                appClientDomains.AddAppClientDomain(() => sp.GetRequiredService<HubClientAppClientDomain>());
                return appClientDomains;
            }
        );
    }

    public static void AddXtiTokenAccessorFactory(this IServiceCollection services, Action<IServiceProvider, XtiTokenAccessorFactory> configure)
    {
        var existing = services.FirstOrDefault(s => s.ImplementationType == typeof(XtiTokenAccessorFactory));
        if (existing != null)
        {
            services.Remove(existing);
        }
        services.AddScoped
        (
            sp =>
            {
                var cache = sp.GetRequiredService<IMemoryCache>();
                var xtiTokenAccessorFactory = new XtiTokenAccessorFactory(cache);
                configure(sp, xtiTokenAccessorFactory);
                return xtiTokenAccessorFactory;
            }
        );
    }

}