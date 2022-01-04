using HubSetupApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Secrets;
using XTI_Configuration.Extensions;
using XTI_Core;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubDB.Extensions;
using XTI_HubSetup;
using XTI_Secrets.Extensions;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.UseXtiConfiguration(hostingContext.HostingEnvironment, args);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHubDbContextForSqlServer(hostContext.Configuration);
        services.AddFileSecretCredentials(hostContext.HostingEnvironment);
        services.AddSingleton<SystemUserCredentials>();
        services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
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
        services.AddScoped
        (
            sp => new HubAppSetup
            (
                sp.GetRequiredService<AppFactory>(),
                sp.GetRequiredService<IClock>(),
                sp.GetRequiredService<HubAppApiFactory>(),
                sp.GetRequiredService<VersionReader>(),
                sp.GetRequiredService<IOptions<SetupOptions>>().Value.Domain
            )
        );
        services.AddHostedService<SetupHostedService>();
        services.AddScoped<AppApiFactory, HubAppApiFactory>();
    })
    .RunConsoleAsync();