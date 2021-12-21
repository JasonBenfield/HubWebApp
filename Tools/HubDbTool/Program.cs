using HubDbTool;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Configuration.Extensions;
using XTI_DB;
using XTI_HubDB.EF;
using XTI_HubDB.Extensions;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.UseXtiConfiguration(hostingContext.HostingEnvironment, args);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<MainDbToolOptions>(hostContext.Configuration);
        services.Configure<DbOptions>(hostContext.Configuration.GetSection(DbOptions.DB));
        services.AddHubDbContextForSqlServer(hostContext.Configuration);
        services.AddScoped<HubDbReset>();
        services.AddScoped<HubDbBackup>();
        services.AddScoped<HubDbRestore>();
        services.AddHostedService<HostedService>();
    })
    .RunConsoleAsync();