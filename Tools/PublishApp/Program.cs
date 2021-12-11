using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using XTI_Configuration.Extensions;
using XTI_PublishTool;
using XTI_Secrets.Extensions;
using XTI_Tool.Extensions;

namespace PublishApp
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
                        services.AddHubToolServices(hostContext.Configuration);
                        services.AddFileSecretCredentials(hostContext.HostingEnvironment);
                        services.AddScoped<GitFactory, DefaultGitFactory>();
                        services.Configure<PublishOptions>(hostContext.Configuration);
                        services.AddHostedService<PublishHostedService>();
                    }
                )
                .RunConsoleAsync();
    }
}
