using HubDbTool;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_DB;
using XTI_Hub;
using XTI_HubDB.EF;
using XTI_HubDB.Extensions;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.UseXtiConfiguration(hostingContext.HostingEnvironment, "", "", args);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton(_ => new XtiEnvironment(hostContext.HostingEnvironment.EnvironmentName));
        services.AddConfigurationOptions<HubDbToolOptions>();
        services.AddConfigurationOptions<DbOptions>(DbOptions.DB);
        services.AddHubDbContextForSqlServer();
        services.AddScoped<DbAdmin<HubDbContext>>();
        services.AddScoped<HubFactory>();
        services.AddScoped<InitialSetup>();
        services.AddHostedService<HostedService>();
    })
    .RunConsoleAsync();