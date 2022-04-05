﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Extensions;
using XTI_HubAppClient.Extensions;
using XTI_TempLog;
using XTI_WebApp.Abstractions;
using XTI_WebApp.Extensions;

namespace XTI_HubAppClient.WebApp.Extensions;

public static class WebAppExtensions
{
    public static void AddThrottledLog<TAppApi>(this IServiceCollection services, Action<TAppApi, ThrottledLogsBuilder> action)
        where TAppApi : IAppApi =>
        AppExtensions.AddThrottledLog(services, action);

    public static ThrottledPathBuilder Throttle(this ThrottledLogsBuilder builder, IAppApiAction action) =>
        AppExtensions.Throttle(builder, action);

    public static ThrottledPathBuilder AndThrottle(this ThrottledPathBuilder builder, IAppApiAction action) =>
        AppExtensions.AndThrottle(builder, action);

    public static void AddAppClients(this IServiceCollection services, Action<IServiceProvider, AppClients> configure)
    {
        var existing = services.FirstOrDefault(s => s.ImplementationType == typeof(AppClients));
        if (existing != null)
        {
            services.Remove(existing);
        }
        services.AddScoped
        (
            sp =>
            {
                var cache = sp.GetRequiredService<IMemoryCache>();
                var appClientDomains = sp.GetRequiredService<AppClientDomainSelector>();
                var appClients = new AppClients(cache, appClientDomains);
                var hubVersion = sp.GetRequiredService<HubAppClientVersion>();
                appClients.AddAppVersion("Hub", hubVersion.Value);
                var appKey = sp.GetRequiredService<AppKey>();
                var versionKey = sp.GetRequiredService<AppVersionKey>();
                appClients.AddAppVersion(appKey.Name.DisplayText, versionKey.DisplayText);
                configure(sp, appClients);
                return appClients;
            }
        );
    }

    public static void AddAppClientDomainSelector(this IServiceCollection services, Action<IServiceProvider, AppClientDomainSelector> configure)
    {
        HubClientExtensions.AddAppClientDomainSelector(services, (sp, domains) =>
        {
            domains.AddAppClientDomain(() => sp.GetRequiredService<SelfAppClientDomain>());
            configure(sp, domains);
        });
    }

    public static void SetDefaultJsonOptions(this JsonOptions options) =>
        XTI_WebApp.Extensions.WebAppExtensions.SetDefaultJsonOptions(options);

    public static void SetDefaultMvcOptions(this MvcOptions options) =>
        XTI_WebApp.Extensions.WebAppExtensions.SetDefaultMvcOptions(options);
}