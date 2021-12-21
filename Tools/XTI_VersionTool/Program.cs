using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Configuration.Extensions;
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
        services.AddScoped<GitFactory, DefaultGitFactory>();
        services.AddScoped<VersionCommandFactory>();
        services.AddHostedService<VersionHostedService>();
    })
    .RunConsoleAsync();