using HubWebAppApi;
using MainDB.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;
using XTI_Configuration.Extensions;
using XTI_Core;
using XTI_AppSetupApp.Extensions;

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
                    services.AddAppSetupServices(hostContext.Configuration);
                    services.AddScoped<AppApiFactory, HubAppApiFactory>();
                    services.AddScoped<IAppSetup, HubSetup>();
                })
                .RunConsoleAsync();
        }
    }
}
