using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_HubAppClient;
using XTI_HubAppClient.ServiceApp.Extensions;
using XTI_PermanentLog;
using XTI_Schedule;
using XTI_SupportServiceAppApi;
using XTI_TempLog;
using XTI_TempLog.Abstractions;
using XTI_TempLog.Extensions;

await XtiServiceAppHost.CreateDefault(SupportInfo.AppKey, args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSupportAppApiServices();
        services.AddScoped<AppApiFactory, SupportAppApiFactory>();
        services.AddScoped(sp => (SupportAppApi)sp.GetRequiredService<IAppApi>());
        services.AddScoped<ITempLogs>(sp =>
        {
            var dataProtector = sp.GetDataProtector("XTI_TempLog");
            var hostEnv = sp.GetRequiredService<IHostEnvironment>();
            var appKey = sp.GetRequiredService<AppKey>();
            var appDataFolder = sp.GetRequiredService<XtiFolder>().AppDataFolder();
            return new DiskTempLogs(dataProtector, appDataFolder.Path(), "TempLogs");
        });
        services.AddScoped<IPermanentLogClient, PermanentLogClient>();
        services.AddScoped<TempToPermanentLog>();
        services.AddAppAgenda
        (
            (sp, agenda) =>
            {
                agenda.AddScheduled<SupportAppApi>
                (
                    (api, agendaItem) =>
                    {
                        agendaItem.Action(api.PermanentLog.MoveToPermanent.Path)
                            .Interval(TimeSpan.FromMinutes(5))
                            .AddSchedule
                            (
                                Schedule.EveryDay().At(TimeRange.AllDay())
                            );
                    }
                );
                agenda.AddScheduled<SupportAppApi>
                (
                    (api, agendaItem) =>
                    {
                        agendaItem.Action(api.PermanentLog.Retry.Path)
                            .DelayAfterStart(TimeSpan.FromMinutes(1))
                            .Interval(TimeSpan.FromHours(1))
                            .AddSchedule
                            (
                                Schedule.EveryDay().At(TimeRange.AllDay())
                            );
                    }
                );
            }
        );
        services.AddThrottledLog<SupportAppApi>
        (
            (api, throttledLogs) =>
            {
                throttledLogs.Throttle(api.PermanentLog.MoveToPermanent)
                    .Requests().ForOneHour()
                    .Exceptions().For(5).Minutes();
            }
        );
    })
    .UseWindowsService()
    .Build()
    .RunAsync();