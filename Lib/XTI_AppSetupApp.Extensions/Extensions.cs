using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using XTI_App.Api;
using XTI_Core;
using XTI_HubAppClient.Extensions;

namespace XTI_AppSetupApp.Extensions
{
    public static class Extensions
    {
        public static void AddAppSetupServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddHubClientServices();
            services.AddScoped(sp =>
            {
                var apiFactory = sp.GetService<AppApiFactory>();
                var template = apiFactory.CreateTemplate();
                return template.AppKey;
            });
            services.Configure<SetupOptions>(config.GetSection(SetupOptions.Setup));
            services.AddScoped(sp =>
            {
                var options = sp.GetService<IOptions<SetupOptions>>().Value;
                return new VersionReader(options.VersionsPath);
            });
            services.AddScoped<DefaultAppSetup>();
            services.AddHostedService<SetupHostedService>();
        }
    }
}
