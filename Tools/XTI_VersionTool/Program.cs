using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Configuration.Extensions;
using XTI_Git;
using XTI_Git.Abstractions;
using XTI_Git.GitLib;
using XTI_Git.Secrets;
using XTI_GitHub;
using XTI_GitHub.Web;
using XTI_Secrets.Extensions;
using XTI_Tool.Extensions;
using XTI_Version;
using XTI_VersionTool;
using XTI_VersionToolApi;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.UseXtiConfiguration(hostingContext.HostingEnvironment, args);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHubToolServices(hostContext.Configuration);
        services.AddFileSecretCredentials(hostContext.HostingEnvironment);
        services.Configure<VersionToolOptions>(hostContext.Configuration);
        services.AddScoped<IGitHubCredentialsAccessor, SecretGitHubCredentialsAccessor>();
        services.AddScoped<IGitHubFactory, WebGitHubFactory>();
        services.AddScoped<GitLibCredentials>();
        services.AddScoped<IXtiGitFactory, GitLibFactory>();
        services.AddScoped<VersionGitFactory>();
        services.AddScoped<VersionCommandFactory>();
        services.AddHostedService<VersionHostedService>();
    })
    .RunConsoleAsync();