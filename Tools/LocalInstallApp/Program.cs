using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_Configuration.Extensions;
using XTI_Core;
using XTI_Hub;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;
using XTI_HubDB.Extensions;
using XTI_Secrets.Extensions;

namespace LocalInstallApp
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
                        services.Configure<InstallOptions>(hostContext.Configuration);
                        services.AddSingleton<XtiFolder>();
                        services.AddScoped<GitFactory>();
                        services.AddFileSecretCredentials(hostContext.HostingEnvironment);
                        services.AddHubClientServices();
                        services.AddHubDbContextForSqlServer(hostContext.Configuration);
                        services.AddScoped<AppFactory>();
                        services.AddScoped<InstallationServiceFactory>();
                        services.AddHostedService<InstallHostedService>();
                    }
                )
                .Build()
                .RunAsync();
    }
}
