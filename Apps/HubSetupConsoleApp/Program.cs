using HubWebApp.Api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_Configuration.Extensions;
using XTI_ConsoleApp.Extensions;

namespace HubSetupConsoleApp
{
    class Program
    {
        static Task Main(string[] args)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.UseXtiConfiguration(hostingContext.HostingEnvironment.EnvironmentName, args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddConsoleAppServices(hostContext.Configuration);
                    services.AddScoped<AppSetup>();
                    services.AddScoped<HubSetup>();
                    services.AddHostedService(sp =>
                    {
                        var scope = sp.CreateScope();
                        var lifetime = scope.ServiceProvider.GetService<IHostApplicationLifetime>();
                        var appSetup = scope.ServiceProvider.GetService<AppSetup>();
                        var hubSetup = scope.ServiceProvider.GetService<HubSetup>();
                        Console.WriteLine($"Environment {hostContext.HostingEnvironment.EnvironmentName}");
                        return new HubSetupService(lifetime, appSetup, hubSetup);
                    });
                })
                .RunConsoleAsync();
        }
    }
}
