using HubSetupApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_Hub;
using XTI_HubWebAppApi;
using XTI_HubDB.Extensions;
using XTI_Secrets.Extensions;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.UseXtiConfiguration(hostingContext.HostingEnvironment, HubInfo.AppKey.Name.DisplayText, HubInfo.AppKey.Type.DisplayText, args);
    })
    .ConfigureServices((hostContext, services) =>
    {
        var xtiEnv = XtiEnvironment.Parse(hostContext.HostingEnvironment.EnvironmentName);
        services.AddSingleton(_ => xtiEnv);
        services.AddSingleton<XtiFolder>();
        services.AddSingleton(_ => HubInfo.AppKey);
        services.AddConfigurationOptions<DbOptions>(DbOptions.DB);
        services.AddHubDbContextForSqlServer();
        services.AddFileSecretCredentials(xtiEnv);
        services.AddScoped<SystemUserCredentials>();
        services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
        services.AddScoped<HubFactory>();
        services.AddScoped<IClock, UtcClock>();
        services.AddScoped<HubAppApiFactory>();
        services.AddScoped(sp => sp.GetRequiredService<HubAppApiFactory>().CreateForSuperUser());
        services.AddConfigurationOptions<SetupOptions>();
        services.AddScoped
        (
            sp => new HubAppSetup
            (
                sp.GetRequiredService<HubFactory>(),
                sp.GetRequiredService<HubAppApiFactory>()
            )
        );
        services.AddHostedService<SetupHostedService>();
        services.AddScoped<AppApiFactory, HubAppApiFactory>();
    })
    .RunConsoleAsync();