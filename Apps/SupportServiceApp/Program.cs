using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using SupportServiceApp.Extensions;
using XTI_Configuration.Extensions;
using XTI_Schedule;

namespace SupportServiceApp
{
    class Program
    {
        public static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                .Build();
            var options = host.Services.GetService<IOptions<AppActionOptions>>();
            return host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.UseXtiConfiguration(context.HostingEnvironment, args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSupportServiceAppServices(hostContext.Configuration);
                })
                .UseWindowsService();
    }
}
