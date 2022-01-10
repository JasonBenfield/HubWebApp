using HubWebApp.ApiControllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Extensions;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.Auth;
using XTI_HubAppApi.PermanentLog;
using XTI_HubDB.Extensions;
using XTI_WebApp.Abstractions;
using XTI_WebApp.Extensions;

namespace HubWebApp.Extensions;

public static class HubWebAppExtensions
{
    public static void AddServicesForHub(this IServiceCollection services, IHostEnvironment hostEnv, IConfiguration configuration)
    {
        AddBasicServicesForHub(services, hostEnv, configuration);
        services
            .AddMvc()
            .AddJsonOptions(options =>
            {
                options.SetDefaultJsonOptions();
            })
            .AddMvcOptions(options =>
            {
                options.SetDefaultMvcOptions();
            });
        services.AddControllersWithViews()
            .PartManager.ApplicationParts.Add
            (
                new AssemblyPart(typeof(UsersController).Assembly)
            );
    }

    public static void AddBasicServicesForHub(this IServiceCollection services, IHostEnvironment hostEnv, IConfiguration configuration)
    {
        services.AddWebAppServices(hostEnv, configuration);
        services.AddHubDbContextForSqlServer(configuration);
        services.AddScoped<AppFactory>();
        services.AddScoped<PermanentLog>();
        services.AddScoped<ISourceUserContext, WebUserContext>();
        services.AddScoped<ISourceAppContext, DefaultAppContext>();
        services.AddScoped<AppFromPath>();
        services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
        services.AddScoped<AccessForAuthenticate, JwtAccess>();
        services.AddScoped<AccessForLogin, CookieAccess>();
        services.AddSingleton(_ => HubInfo.AppKey);
        services.AddScoped<AppApiFactory, HubAppApiFactory>();
        services.AddScoped(sp => (HubAppApi)sp.GetRequiredService<IAppApi>());
        services.AddHubAppApiServices();
        services.AddScoped
        (
            sp =>
            {
                var appClientDomains = new AppClientDomainSelector();
                appClientDomains.AddAppClientDomain(() => sp.GetRequiredService<SelfAppClientDomain>());
                return appClientDomains;
            }
        );
        services.AddScoped
        (
            sp =>
            {
                var cache = sp.GetRequiredService<IMemoryCache>();
                var appClientDomains = sp.GetRequiredService<AppClientDomainSelector>();
                var appClients = new AppClients(cache, appClientDomains);
                var appKey = sp.GetRequiredService<AppKey>();
                var versionKey = sp.GetRequiredService<AppVersionKey>();
                appClients.AddAppVersion(appKey.Name.DisplayText, versionKey.DisplayText);
                return appClients;
            }
        );
        services.AddThrottledLog<HubAppApi>
        (
            (api, throttledLogs) =>
            {
                throttledLogs.Throttle(api.PermanentLog.LogBatch.Path.Format())
                    .Requests().ForOneHour()
                    .Exceptions().For(5).Minutes();
            }
        );
    }
}