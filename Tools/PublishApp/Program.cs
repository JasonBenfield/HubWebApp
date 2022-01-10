using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PublishApp;
using XTI_Configuration.Extensions;
using XTI_Git.Abstractions;
using XTI_Git.Secrets;
using XTI_GitHub;
using XTI_GitHub.Web;
using XTI_PublishTool;
using XTI_Secrets.Extensions;
using XTI_Tool.Extensions;

await Host.CreateDefaultBuilder(args)
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
            services.AddScoped<IGitHubCredentialsAccessor, SecretGitHubCredentialsAccessor>();
            services.AddScoped<IGitHubFactory, WebGitHubFactory>();
            services.Configure<PublishOptions>(hostContext.Configuration);
            services.AddHostedService<PublishHostedService>();
        }
    )
    .RunConsoleAsync();