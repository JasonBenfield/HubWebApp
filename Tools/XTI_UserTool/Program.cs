using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_Configuration.Extensions;
using XTI_Secrets.Extensions;
using XTI_Tool.Extensions;

namespace XTI_UserApp
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
                    services.AddHubToolServices(hostContext.Configuration);
                    services.AddFileSecretCredentials(hostContext.HostingEnvironment);
                    services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
                    services.Configure<UserOptions>(hostContext.Configuration);
                    services.AddHostedService<HostedService>();
                })
                .RunConsoleAsync();
        }
    }
}
