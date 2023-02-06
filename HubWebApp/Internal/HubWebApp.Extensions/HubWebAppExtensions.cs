using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Extensions;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_HubWebAppApi;
using XTI_HubWebAppApi.PermanentLog;
using XTI_HubDB.Extensions;
using XTI_WebApp.Abstractions;
using XTI_WebApp.Extensions;
using XTI_App.Hosting;
using XTI_Schedule;
using XTI_Core;
using XTI_WebAppClient;
using XTI_WebApp.Api;

namespace HubWebApp.Extensions;

public static class HubWebAppExtensions
{
    public static void AddServicesForHub(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddAppServices();
        services.AddWebAppServices();
        services.AddSingleton
        (
            _ => new AppPageModel
            {
                PostStyleSheets = new[]
                {
                    "~/styles/css/Hub.css"
                }
            }
        );
        services.AddHubDbContextForSqlServer();
        services.AddScoped<HubFactory>();
        services.AddScoped<PermanentLog>();
        services.AddScoped<ISourceUserContext, WebUserContext>();
        services.AddScoped<IUserProfileUrl, DefaultUserProfileUrl>();
        services.AddScoped<EfAppContext>();
        services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<EfAppContext>());
        services.AddScoped<AppFromPath>();
        services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
        services.AddScoped<AccessForAuthenticate, JwtAccess>();
        services.AddScoped<AccessForLogin, CookieAccess>();
        services.AddSingleton(_ => HubInfo.AppKey);
        services.AddScoped<AppApiFactory, HubAppApiFactory>();
        services.AddScoped(sp => (HubAppApi)sp.GetRequiredService<IAppApi>());
        services.AddScoped<IHubAdministration, EfHubAdministration>();
        services.AddScoped<IStoredObjectDB, EfStoredObjectDB>();
        services.AddScoped<StoredObjectFactory>();
        services.AddScoped<ILoginReturnKey, LoginReturnKey>();
        services.AddScoped<IMenuDefinitionBuilder, HubMenuDefinitionBuilder>();
        services.AddScoped<AuthenticationFactory>();
        services.AddScoped<IAuthClient, DefaultAuthClient>();
        services.AddScoped<SystemUserXtiToken>();
        services.AddScoped
        (
            sp =>
            {
                var tokenAccessor = new XtiTokenAccessor(sp.GetRequiredService<IMemoryCache>());
                tokenAccessor.AddToken(() => sp.GetRequiredService<SystemUserXtiToken>());
                tokenAccessor.UseToken<SystemUserXtiToken>();
                return tokenAccessor;
            }
        );
        services.AddScoped<IAppClientDomain>(sp => sp.GetRequiredService<AppClientDomainSelector>());
        services.AddScoped<AppClientUrl>();
        services.AddScoped<GenericAppClientFactory>();
        services.AddScoped<IUserCacheManagement, DefaultUserCacheManagement>();
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
        services.AddAppAgenda
        (
            (sp, agenda) =>
            {
                agenda.AddScheduled<HubAppApi>
                (
                    (api, agendaItem) =>
                    {
                        agendaItem.Action(api.Periodic.PurgeLogs)
                            .Interval(TimeSpan.FromHours(7))
                            .AddSchedule
                            (
                                Schedule.EveryDay().At(TimeRange.AllDay())
                            );
                    }
                );
            }
        );
        services.AddThrottledLog<HubAppApi>
        (
            (api, throttledLogs) =>
            {
                throttledLogs.Throttle(api.PermanentLog.LogBatch)
                    .Requests().ForOneHour()
                    .Exceptions().For(5).Minutes();
            }
        );
    }
}