using XTI_Core;
using XTI_Schedule;

namespace XTI_HubWebAppApi.Periodic;

partial class PeriodicGroupBuilder
{
    partial void Configure()
    {
        DeleteExpiredStoredObjects
            .RunContinuously()
                .Interval(TimeSpan.FromHours(1))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                );
        EndExpiredSessions
            .RunContinuously()
                .Interval(TimeSpan.FromHours(1))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                );
        PurgeLogs
            .RunContinuously()
                .Interval(TimeSpan.FromHours(7))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                );
    }
}
