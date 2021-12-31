using Microsoft.Extensions.Caching.Memory;
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
    public static void AddHubClientServices(this IServiceCollection services)
    {
        services.AddHttpClient();
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
        services.AddScoped(sp =>
        {
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var xtiTokenFactory = sp.GetRequiredService<IXtiTokenFactory>();
            var appOptions = sp.GetRequiredService<IOptions<AppOptions>>().Value;
            var env = sp.GetRequiredService<IHostEnvironment>();
            var versionKey = env.IsProduction() 
                ? "" 
                : XTI_App.Abstractions.AppVersionKey.Current.Value;
            return new HubAppClient(httpClientFactory, xtiTokenFactory, appOptions.BaseUrl, versionKey);
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