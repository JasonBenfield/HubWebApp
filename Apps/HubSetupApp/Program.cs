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
using XTI_HubSetup;

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
                    services.AddScoped(sp => sp.GetService<HubAppApiFactory>().CreateForSuperUser());
                    services.Configure<SetupOptions>(hostContext.Configuration.GetSection(SetupOptions.Setup));
                    services.AddScoped(sp =>
                    {
                        var options = sp.GetService<IOptions<SetupOptions>>().Value;
                        return new VersionReader(options.VersionsPath);
                    });
                    services.AddScoped<HubAppSetup>();
                    services.AddHostedService<SetupHostedService>();
                    services.AddScoped<AppApiFactory, HubAppApiFactory>();
                })
                .RunConsoleAsync();
        }
    }
}
