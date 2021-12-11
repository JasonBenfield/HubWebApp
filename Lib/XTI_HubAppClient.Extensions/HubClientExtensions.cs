using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Net.Http;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Secrets;
using XTI_TempLog.Abstractions;
using XTI_WebAppClient;

namespace XTI_HubAppClient.Extensions
{
    public static class HubClientExtensions
    {
        public static void AddHubClientServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped(sp =>
            {
                var credentials = sp.GetService<ISystemUserCredentials>();
                return new XtiTokenFactory(credentials);
            });
            services.AddScoped<IXtiTokenFactory>(sp =>
            {
                var cache = sp.GetService<IMemoryCache>();
                var xtiTokenFactory = sp.GetService<XtiTokenFactory>();
                return new CachedXtiTokenFactory(cache, xtiTokenFactory);
            });
            services.AddScoped(sp =>
            {
                var httpClientFactory = sp.GetService<IHttpClientFactory>();
                var xtiTokenFactory = sp.GetService<IXtiTokenFactory>();
                var appOptions = sp.GetService<IOptions<AppOptions>>().Value;
                var env = sp.GetService<IHostEnvironment>();
                var versionKey = env.IsProduction() ? "" : AppVersionKey.Current.Value;
                return new HubAppClient(httpClientFactory, xtiTokenFactory, appOptions.BaseUrl, versionKey);
            });
            services.AddScoped<IAuthClient>(sp => sp.GetService<HubAppClient>());
            services.AddScoped(sp =>
            {
                var xtiTokenFactory = sp.GetService<IXtiTokenFactory>();
                var authClient = sp.GetService<IAuthClient>();
                return xtiTokenFactory.Create(authClient);
            });
            services.AddScoped<IPermanentLogClient, PermanentLogClient>();
        }
    }
}
