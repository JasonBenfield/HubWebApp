using XTI_Core;
using XTI_Schedule;

namespace XTI_SupportServiceAppApi.Installations;

partial class InstallationsGroupBuilder
{
    partial void Configure()
    {
        ExecutePendingCommands
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(15).Minutes()
            .RunContinuously()
            .Interval(TimeSpan.FromMinutes(1))
            .AddSchedule
            (
                Schedule.EveryDay().At(TimeRange.AllDay())
            );
    }
}
