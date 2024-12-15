using XTI_Core;
using XTI_Schedule;

namespace XTI_SupportServiceAppApi.PermanentLog;

partial class PermanentLogGroupBuilder
{
    partial void Configure()
    {
        MoveToPermanent
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(5).Minutes()
            .RunContinuously()
                .Interval(TimeSpan.FromMinutes(2))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                );
        MoveToPermanentV1
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(5).Minutes()
            .RunContinuously()
                .Interval(TimeSpan.FromMinutes(5))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                );
        Retry
            .RunContinuously()
                .DelayAfterStart(TimeSpan.FromMinutes(1))
                .Interval(TimeSpan.FromHours(1))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                );
        RetryV1
            .RunContinuously()
                .DelayAfterStart(TimeSpan.FromMinutes(1))
                .Interval(TimeSpan.FromHours(1))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                );
    }
}
