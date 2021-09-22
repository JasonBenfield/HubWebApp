using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Configuration.Extensions;
using XTI_Hub;
using XTI_Secrets.Extensions;
using XTI_Tool.Extensions;

namespace InstallApp
{
    class Program
    {
        static Task Main(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration
                (
                    (hostContext, configuration) =>
                    {
                        configuration.UseXtiConfiguration(hostContext.HostingEnvironment, args);
                    }
                )
                .ConfigureServices
                (
                    (hostContext, services) =>
                    {
                        services.AddXtiDataProtection();
                        services.AddHttpClient();
                        services.AddFileSecretCredentials();
                        services.AddHubToolServices(hostContext.Configuration);
                        services.Configure<InstallOptions>(hostContext.Configuration);
                        services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
                        services.AddHostedService<InstallHostedService>();
                    }
                )
                .Build()
                .RunAsync();
    }
}
