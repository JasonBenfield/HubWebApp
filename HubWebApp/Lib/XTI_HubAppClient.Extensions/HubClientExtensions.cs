using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
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
        services.AddXtiTokenAccessor((sp, accessor) => { });
        services.AddScoped<HubClientOptionsAppClientDomain>();
        services.AddScoped<HubClientAppClientDomain>();
        if (!services.Any(s => s.ImplementationType == typeof(AppClientDomainSelector)))
        {
            AddAppClientDomainSelector(services, (sp, domains) => { });
        }
        services.AddScoped<IAppClientDomain>(sp => sp.GetRequiredService<AppClientDomainSelector>());
        services.AddScoped<AppClientUrl>();
        services.AddSingleton<HubAppClientVersion>();
        services.AddScoped<HubAppClient>();
        services.AddScoped<IAuthClient>(sp => sp.GetRequiredService<HubAppClient>());
        services.AddScoped<IPermanentLogClient, PermanentLogClient>();
        services.AddScoped<HubAppClientContext>();
        services.AddScoped(sp => sp.GetRequiredService<HubAppClientContext>().AppContext);
        services.AddScoped(sp => sp.GetRequiredService<HubAppClientContext>().UserContext);
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

    public static void AddXtiTokenAccessor(this IServiceCollection services, Action<IServiceProvider, XtiTokenAccessor> configure)
    {
        var existing = services.FirstOrDefault(s => s.ImplementationType == typeof(XtiTokenAccessor));
        if (existing != null)
        {
            services.Remove(existing);
        }
        services.AddScoped
        (
            sp =>
            {
                var cache = sp.GetRequiredService<IMemoryCache>();
                var xtiTokenAccessor = new XtiTokenAccessor(cache);
                configure(sp, xtiTokenAccessor);
                return xtiTokenAccessor;
            }
        );
    }

}