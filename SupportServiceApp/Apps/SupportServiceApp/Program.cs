using Microsoft.Extensions.Hosting;
using SupportServiceApp.Extensions;
using XTI_Core;
using XTI_HubAppClient.ServiceApp.Extensions;
using XTI_Schedule;
using XTI_SupportServiceAppApi;

await XtiServiceAppHost.CreateDefault(SupportInfo.AppKey, args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSupportServiceAppServices();
        services.AddAppAgenda
        (
            (sp, agenda) =>
            {
                agenda.AddScheduled<SupportAppApi>
                (
                    (api, agenda) =>
                    {
                        agenda.Action(api.Home.DoSomething.Path)
                            .Interval(TimeSpan.FromMinutes(5))
                            .AddSchedule
                            (
                                Schedule.EveryDay().At(TimeRange.AllDay())
                            );
                    }
                );
            }
        );
    })
    .RunConsoleAsync();