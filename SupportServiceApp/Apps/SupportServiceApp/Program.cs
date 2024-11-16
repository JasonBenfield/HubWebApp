﻿using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;
using XTI_HubAppClient.ServiceApp.Extensions;
using XTI_PermanentLog;
using XTI_PermanentLog.Implementations;
using XTI_Schedule;
using XTI_SupportServiceAppApi;
using XTI_TempLog;
using XTI_TempLog.Extensions;
using XTI_HubDB.Extensions;

var hostBuilder = XtiServiceAppHost.CreateDefault(SupportInfo.AppKey, args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSupportAppApiServices();
        services.AddScoped<AppApiFactory, SupportAppApiFactory>();
        services.AddScoped(sp => (SupportAppApi)sp.GetRequiredService<IAppApi>());
        services.AddScoped<ITempLogsV1>(sp =>
        {
            var dataProtector = sp.GetDataProtector("XTI_TempLog");
            var appKey = sp.GetRequiredService<AppKey>();
            var appDataFolder = sp.GetRequiredService<XtiFolder>().AppDataFolder();
            return new DiskTempLogsV1(dataProtector, appDataFolder.Path(), "TempLogs");
        });
        services.AddHubDbContextForSqlServer();
        services.AddScoped<HubFactory>();
        services.AddScoped<EfPermanentLog>();
        services.AddScoped<HcPermanentLog>();
        services.AddScoped
        (
            sp =>
            {
                IPermanentLog permanentLog;
                var options = sp.GetRequiredService<SupportServiceAppOptions>();
                if (options.PermanentLogType == "DB")
                {
                    permanentLog = sp.GetRequiredService<EfPermanentLog>();
                }
                else
                {
                    permanentLog = sp.GetRequiredService<HcPermanentLog>();
                }
                return permanentLog;
            }
        );
        services.AddScoped<TempToPermanentLog>();
        services.AddScoped<TempToPermanentLogV1>();
        services.AddAppAgenda
        (
            (sp, agenda) =>
            {
                agenda.AddScheduled<SupportAppApi>
                (
                    (api, agendaItem) =>
                    {
                        agendaItem.Action(api.PermanentLog.MoveToPermanent)
                            .Interval(TimeSpan.FromMinutes(2))
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
                        agendaItem.Action(api.PermanentLog.MoveToPermanentV1)
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
                        agendaItem.Action(api.PermanentLog.Retry)
                            .DelayAfterStart(TimeSpan.FromMinutes(1))
                            .Interval(TimeSpan.FromHours(1))
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
                        agendaItem.Action(api.PermanentLog.RetryV1)
                            .DelayAfterStart(TimeSpan.FromMinutes(1))
                            .Interval(TimeSpan.FromHours(1))
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
                        agendaItem.Action(api.Installations.Delete)
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
                throttledLogs.Throttle(api.PermanentLog.MoveToPermanentV1)
                    .Requests().ForOneHour()
                    .Exceptions().For(5).Minutes();
            }
        );
    });
if (args.Length <= 0 || !args[0].Equals("RunAsConsole", StringComparison.OrdinalIgnoreCase))
{
    hostBuilder.UseWindowsService();
}
await hostBuilder.Build().RunAsync();