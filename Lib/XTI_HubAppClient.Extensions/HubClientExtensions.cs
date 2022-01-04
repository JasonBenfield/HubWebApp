using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_App.Abstractions;
using XTI_App.Secrets;
using XTI_TempLog.Abstractions;
using XTI_WebAppClient;

namespace XTI_HubAppClient.Extensions;

public static class HubClientExtensions
{
    public static void AddHubClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.Configure<HubClientOptions>(configuration.GetSection(HubClientOptions.HubClient));
        services.AddScoped(sp =>
        {
            var credentials = sp.GetRequiredService<ISystemUserCredentials>();
            return new XtiTokenFactory(credentials);
        });
        services.AddScoped<IXtiTokenFactory>(sp =>
        {
            var cache = sp.GetRequiredService<IMemoryCache>();
            var xtiTokenFactory = sp.GetRequiredService<XtiTokenFactory>();
            return new CachedXtiTokenFactory(cache, xtiTokenFactory);
        });
        services.AddSingleton<HubClientOptionsAppClientDomain>();
        services.AddScoped(sp =>
        {
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var xtiTokenFactory = sp.GetRequiredService<IXtiTokenFactory>();
            var env = sp.GetRequiredService<IHostEnvironment>();
            var versionKey = env.IsProduction()
                ? ""
                : AppVersionKey.Current.Value;
            var hubClientOptions = sp.GetRequiredService<IOptions<HubClientOptions>>();
            var clientDomain = sp.GetRequiredService<HubClientOptionsAppClientDomain>();
            var clientUrl = new AppClientUrl(clientDomain);
            return new HubAppClient(httpClientFactory, xtiTokenFactory, clientUrl, versionKey);
        });
        services.AddScoped<IAuthClient>(sp => sp.GetRequiredService<HubAppClient>());
        services.AddScoped(sp =>
        {
            var xtiTokenFactory = sp.GetRequiredService<IXtiTokenFactory>();
            var authClient = sp.GetRequiredService<IAuthClient>();
            return xtiTokenFactory.Create(authClient);
        });
        services.AddScoped<IPermanentLogClient, PermanentLogClient>();
    }
}