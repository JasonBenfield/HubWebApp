using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_App.Extensions;
using XTI_App.Hosting;
using XTI_ConsoleApp;
using XTI_HubAppClient.Extensions;
using XTI_TempLog;
using XTI_WebApp.Abstractions;

namespace XTI_HubAppClient.ConsoleApp.Extensions;

public static class ConsoleAppExtensions
{
    public static void AddAppAgenda(this IServiceCollection services, Action<IServiceProvider, AppAgendaBuilder> build) =>
        services.AddAppAgenda
        (
            (sp, b) =>
            {
                build(sp, b);
                var api = sp.GetRequiredService<IAppApi>();
                if (api is ConsoleAppApiWrapper)
                {
                    b.AddImmediate<ConsoleAppApiWrapper>(consoleApi => consoleApi.Lifetime.StopApplication);
                }
            }
        );

    public static void AddThrottledLog<TAppApi>(this IServiceCollection services, Action<TAppApi, ThrottledLogsBuilder> action)
            where TAppApi : IAppApi =>
            AppExtensions.AddThrottledLog(services, action);

    public static ThrottledPathBuilder Throttle(this ThrottledLogsBuilder builder, IAppApiAction action) =>
        AppExtensions.Throttle(builder, action);

    public static ThrottledPathBuilder AndThrottle(this ThrottledPathBuilder builder, IAppApiAction action) =>
        AppExtensions.AndThrottle(builder, action);

    public static void AddAppClientDomainSelector(this IServiceCollection services, Action<IServiceProvider, AppClientDomainSelector> configure) =>
        HubClientExtensions.AddAppClientDomainSelector(services, configure);
}