using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using XTI_ApiGeneratorApp.Extensions;
using XTI_App.Api;
using XTI_Configuration.Extensions;
using XTI_HubAppApi;

namespace HubApiGeneratorApp
{
    class Program
    {
        static Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.UseXtiConfiguration(hostingContext.HostingEnvironment, args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApiGenerator(hostContext.Configuration);
                    services.AddScoped<AppApiFactory, HubAppApiFactory>();
                    services.AddHostedService<ApiGeneratorHostedService>();
                });
            var host = builder.Build();
            var options = host.Services.GetService<IOptions<OutputOptions>>().Value;
            return host.RunAsync();
        }
    }
}
