using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using XTI_App.Api;
using XTI_Configuration.Extensions;
using XTI_Core;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubDB.Extensions;

namespace HubSetupApp
{
    class Program
    {
        static Task Main(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.UseXtiConfiguration(hostingContext.HostingEnvironment, args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHubDbContextForSqlServer(hostContext.Configuration);
                    services.AddScoped<AppFactory>();
                    services.AddScoped<Clock, UtcClock>();
                    services.AddScoped<HubAppApiFactory>();
                    services.AddScoped(sp => (HubAppApi)sp.GetService<HubAppApiFactory>().CreateForSuperUser());
                    services.Configure<SetupOptions>(hostContext.Configuration.GetSection(SetupOptions.Setup));
                    services.AddScoped(sp =>
                    {
                        var hubApi = sp.GetService<HubAppApi>();
                        var apiFactory = sp.GetService<AppApiFactory>();
                        var appKey = apiFactory.CreateTemplate().AppKey;
                        var options = sp.GetService<IOptions<SetupOptions>>().Value;
                        return new PersistedVersions(hubApi, appKey, options.VersionsPath);
                    });
                    services.AddScoped<HubAppSetup>();
                    services.AddHostedService<SetupHostedService>();
                    services.AddScoped<AppApiFactory, HubAppApiFactory>();
                })
                .RunConsoleAsync();
        }
    }
}
