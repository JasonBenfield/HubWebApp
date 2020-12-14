using HubWebApp.Api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.EF;
using XTI_App.Extensions;
using XTI_Configuration.Extensions;
using XTI_Core;

namespace HubSetupConsoleApp
{
    class Program
    {
        static Task Main(string[] args)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.UseXtiConfiguration(hostingContext.HostingEnvironment, args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddAppDbContextForSqlServer(hostContext.Configuration);
                    services.AddScoped<Clock, UtcClock>();
                    services.AddScoped<AppFactory, EfAppFactory>();
                    services.AddScoped<AllAppSetup>();
                    services.AddScoped<HubSetup>();
                    services.AddHostedService(sp =>
                    {
                        var scope = sp.CreateScope();
                        var lifetime = scope.ServiceProvider.GetService<IHostApplicationLifetime>();
                        var appSetup = scope.ServiceProvider.GetService<AllAppSetup>();
                        var hubSetup = scope.ServiceProvider.GetService<HubSetup>();
                        Console.WriteLine($"Environment {hostContext.HostingEnvironment.EnvironmentName}");
                        return new HubSetupService(lifetime, appSetup, hubSetup);
                    });
                })
                .RunConsoleAsync();
        }
    }
}
