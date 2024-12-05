using XTI_Core;
using XTI_Schedule;

namespace XTI_SupportServiceAppApi.PermanentLog;

public sealed class PermanentLogGroup : AppApiGroupWrapper
{
    public PermanentLogGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        MoveToPermanent = source.AddAction<EmptyRequest, EmptyActionResult>()
            .Named(nameof(MoveToPermanent))
            .WithExecution<MoveToPermanentAction>()
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(5).Minutes()
            .RunContinuously()
                .Interval(TimeSpan.FromMinutes(2))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                )
            .Build();
        MoveToPermanentV1 = source.AddAction<EmptyRequest, EmptyActionResult>()
            .Named(nameof(MoveToPermanentV1))
            .WithExecution<MoveToPermanentV1Action>()
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(5).Minutes()
            .RunContinuously()
                .Interval(TimeSpan.FromMinutes(5))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                )
            .Build();
        Retry = source.AddAction<EmptyRequest, EmptyActionResult>()
            .Named(nameof(Retry))
            .WithExecution<RetryAction>()
            .RunContinuously()
                .DelayAfterStart(TimeSpan.FromMinutes(1))
                .Interval(TimeSpan.FromHours(1))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                )
            .Build();
        RetryV1 = source.AddAction<EmptyRequest, EmptyActionResult>()
            .Named(nameof(RetryV1))
            .WithExecution<RetryV1Action>()
            .RunContinuously()
                .DelayAfterStart(TimeSpan.FromMinutes(1))
                .Interval(TimeSpan.FromHours(1))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                )
            .Build();
    }

    public AppApiAction<EmptyRequest, EmptyActionResult> MoveToPermanent { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> MoveToPermanentV1 { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> Retry { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> RetryV1 { get; }
}