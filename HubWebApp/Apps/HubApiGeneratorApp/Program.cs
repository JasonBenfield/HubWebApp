using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_ApiGeneratorApp.Extensions;
using XTI_App.Api;
using XTI_Core.Extensions;
using XTI_Hub;
using XTI_HubWebAppApi;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.UseXtiConfiguration(hostingContext.HostingEnvironment, HubInfo.AppKey.Name.DisplayText, HubInfo.AppKey.Type.DisplayText, args);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddApiGenerator();
        services.AddScoped<AppApiFactory, HubAppApiFactory>();
        services.AddHostedService<ApiGeneratorHostedService>();
    })
    .RunConsoleAsync();