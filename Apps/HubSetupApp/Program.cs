using HubSetupApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubDB.Extensions;
using XTI_HubSetup;
using XTI_Secrets.Extensions;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.UseXtiConfiguration(hostingContext.HostingEnvironment, HubInfo.AppKey.Name.DisplayText, HubInfo.AppKey.Type.DisplayText, args);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton(_ => XtiEnvironment.Parse(hostContext.HostingEnvironment.EnvironmentName));
        services.AddSingleton<XtiFolder>();
        services.AddSingleton(_ => HubInfo.AppKey);
        services.AddHubDbContextForSqlServer();
        services.AddFileSecretCredentials();
        services.AddSingleton<SystemUserCredentials>();
        services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
        services.AddScoped<AppFactory>();
        services.AddScoped<IClock, UtcClock>();
        services.AddScoped<HubAppApiFactory>();
        services.AddScoped(sp => sp.GetRequiredService<HubAppApiFactory>().CreateForSuperUser());
        services.AddConfigurationOptions<SetupOptions>();
        services.AddScoped<IVersionReader>(sp =>
        {
            var options = sp.GetRequiredService<SetupOptions>();
            return new FileVersionReader(options.VersionsPath);
        });
        services.AddScoped
        (
            sp => new HubAppSetup
            (
                sp.GetRequiredService<AppFactory>(),
                sp.GetRequiredService<IClock>(),
                sp.GetRequiredService<HubAppApiFactory>(),
                sp.GetRequiredService<IVersionReader>(),
                sp.GetRequiredService<SetupOptions>().Domain
            )
        );
        services.AddHostedService<SetupHostedService>();
        services.AddScoped<AppApiFactory, HubAppApiFactory>();
    })
    .RunConsoleAsync();