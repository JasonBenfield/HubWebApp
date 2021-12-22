using HubSetupApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_App.Api;
using XTI_Configuration.Extensions;
using XTI_Core;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubDB.Extensions;
using XTI_HubSetup;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.UseXtiConfiguration(hostingContext.HostingEnvironment, args);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHubDbContextForSqlServer(hostContext.Configuration);
        services.AddScoped<AppFactory>();
        services.AddScoped<IClock, UtcClock>();
        services.AddScoped<HubAppApiFactory>();
        services.AddScoped(sp => sp.GetRequiredService<HubAppApiFactory>().CreateForSuperUser());
        services.Configure<SetupOptions>(hostContext.Configuration.GetSection(SetupOptions.Setup));
        services.AddScoped(sp =>
        {
            var options = sp.GetRequiredService<IOptions<SetupOptions>>().Value;
            return new VersionReader(options.VersionsPath);
        });
        services.AddScoped<HubAppSetup>();
        services.AddHostedService<SetupHostedService>();
        services.AddScoped<AppApiFactory, HubAppApiFactory>();
    })
    .RunConsoleAsync();