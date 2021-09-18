using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using XTI_AppSetupApp.Extensions;
using XTI_Configuration.Extensions;
using XTI_App.Api;
using XTI_SupportAppApi;

namespace SupportSetupApp
{
    class Program
    {
        static Task Main(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.UseXtiConfiguration(hostingContext.HostingEnvironment, args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddAppSetupServices(hostContext.Configuration);
                    services.AddScoped<AppApiFactory, SupportAppApiFactory>();
                })
                .RunConsoleAsync();
    }
}
