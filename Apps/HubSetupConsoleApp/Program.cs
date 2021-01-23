using HubWebApp.Api;
using MainDB.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;
using XTI_Configuration.Extensions;
using XTI_Core;

namespace HubSetupConsoleApp
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
                    services.AddAppDbContextForSqlServer(hostContext.Configuration);
                    services.AddScoped<Clock, UtcClock>();
                    services.AddScoped<AppFactory>();
                    services.AddScoped<AppApiFactory, HubAppApiFactory>();
                    services.AddScoped<HubSetup>();
                    services.AddHostedService<HubSetupService>();
                })
                .RunConsoleAsync();
        }
    }
}
