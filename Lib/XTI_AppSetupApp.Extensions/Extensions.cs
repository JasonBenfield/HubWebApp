using XTI_HubDB.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;
using XTI_HubAppApi;

namespace XTI_AppSetupApp.Extensions
{
    public static class Extensions
    {
        public static void AddAppSetupServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddHubDbContextForSqlServer(config);
            services.AddScoped<AppFactory>();
            services.AddScoped<Clock, UtcClock>();
            services.AddScoped<HubAppApiFactory>();
            services.AddScoped(sp => (HubAppApi)sp.GetService<HubAppApiFactory>().CreateForSuperUser());
            services.Configure<SetupOptions>(config.GetSection(SetupOptions.Setup));
            services.AddScoped(sp =>
            {
                var hubApi = sp.GetService<HubAppApi>();
                var apiFactory = sp.GetService<AppApiFactory>();
                var appKey = apiFactory.CreateTemplate().AppKey;
                var options = sp.GetService<IOptions<SetupOptions>>().Value;
                return new PersistedVersions(hubApi, appKey, options.VersionsPath);
            });
            services.AddScoped<DefaultAppSetup>();
            services.AddHostedService<SetupHostedService>();
        }
    }
}
