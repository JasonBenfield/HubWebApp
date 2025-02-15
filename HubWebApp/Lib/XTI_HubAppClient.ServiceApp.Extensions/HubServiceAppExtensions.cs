﻿using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_App.Extensions;
using XTI_App.Hosting;
using XTI_HubAppClient.Extensions;
using XTI_TempLog;
using XTI_WebApp.Abstractions;

namespace XTI_HubAppClient.ServiceApp.Extensions;

public static class HubServiceAppExtensions
{
    public static void AddAppAgenda(this IServiceCollection services, Action<IServiceProvider, AppAgendaBuilder> build) =>
        AppAgendaExtensions.AddAppAgenda(services, build);

    public static void AddThrottledLog<TAppApi>(this IServiceCollection services, Action<TAppApi, ThrottledLogsBuilder> action)
        where TAppApi : IAppApi =>
        AppExtensions.AddThrottledLog(services, action);

    public static ThrottledLogPathBuilder Throttle(this ThrottledLogsBuilder builder, IAppApiAction action) =>
        AppExtensions.Throttle(builder, action);

    public static ThrottledLogPathBuilder AndThrottle(this ThrottledLogPathBuilder builder, IAppApiAction action) =>
        AppExtensions.AndThrottle(builder, action);

    public static void AddAppClientDomainSelector(this IServiceCollection services, Action<IServiceProvider, AppClientDomainSelector> configure) =>
        HubClientExtensions.AddAppClientDomainSelector(services, configure);
}