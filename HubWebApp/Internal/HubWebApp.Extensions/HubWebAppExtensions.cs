using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Extensions;
using XTI_Core.Extensions;
using XTI_Hub;
using XTI_HubDB.Extensions;
using XTI_HubWebAppApi;
using XTI_HubWebAppApiActions;
using XTI_WebApp.Abstractions;
using XTI_WebApp.Api;
using XTI_WebApp.Extensions;
using XTI_WebAppClient;

namespace HubWebApp.Extensions;

public static class HubWebAppExtensions
{
    public static void AddServicesForHub(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddAppServices();
        services.AddDefaultWebAppServices();
        services.AddSingleton
        (
            _ => new AppPageModel
            {
                PostStyleSheets =
                [
                    "~/styles/css/Hub.css"
                ]
            }
        );
        services.AddHubDbContextForSqlServer();
        services.AddScoped<HubFactory>();
        services.AddScoped<EfPermanentLog>();
        services.AddScoped<ISourceUserContext, WebUserContext>();
        services.AddScoped<IUserProfileUrl, DefaultUserProfileUrl>();
        services.AddScoped<EfAppContext>();
        services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<EfAppContext>());
        services.AddScoped<AppFromPath>();
        services.AddScoped<IHashedPasswordFactory, Md5HashedPasswordFactory>();
        services.AddScoped<JwtAccess>();
        services.AddKeyedScoped<IAccess>
        (
            "Authenticate", 
            (sp, key) => sp.GetRequiredService<JwtAccess>()
        );
        services.AddKeyedScoped<IAccess>
        (
            "Login",
            (sp, key) => new CookieAccess
            (
                sp.GetRequiredService<IHttpContextAccessor>(),
                sp.GetRequiredService<JwtAccess>()
            )
        );
        services.AddSingleton(_ => HubInfo.AppKey);
        services.AddScoped<AppApiFactory, HubAppApiFactory>();
        services.AddScoped(sp => (HubAppApi)sp.GetRequiredService<IAppApi>());
        services.AddScoped<IHubAdministration, EfHubAdministration>();
        services.AddScoped<ILoginReturnKey, LoginReturnKey>();
        services.AddScoped<IMenuDefinitionBuilder, HubMenuDefinitionBuilder>();
        services.AddScoped<AuthenticationFactory>();
        services.AddScoped<IAuthClient, DefaultAuthClient>();
        services.AddScoped<SystemUserXtiToken>();
        services.AddScoped
        (
            sp =>
            {
                var tokenAccessorFactory = new XtiTokenAccessorFactory(sp.GetRequiredService<IMemoryCache>());
                tokenAccessorFactory.AddToken(() => sp.GetRequiredService<SystemUserXtiToken>());
                tokenAccessorFactory.UseDefaultToken<SystemUserXtiToken>();
                return tokenAccessorFactory;
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
        services.AddConfigurationOptions<HubWebAppOptions>();
        services.AddThrottledLog<HubAppApi>
        (
            (api, throttledLogs) =>
            {
                throttledLogs.Throttle(api.User.GetMenuLinks)
                    .Requests().ForOneHour()
                    .Exceptions().For(5).Minutes();
            }
        );
    }
}