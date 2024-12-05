using XTI_Core;
using XTI_Schedule;

namespace XTI_HubWebAppApi.Periodic;

public sealed class PeriodicGroup : AppApiGroupWrapper
{
    public PeriodicGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        DeactivateUsers = source.AddAction<EmptyRequest, EmptyActionResult>()
            .Named(nameof(DeactivateUsers))
            .WithExecution<DeactivateUsersAction>()
            .Build();
        DeleteExpiredStoredObjects = source.AddAction<EmptyRequest, EmptyActionResult>()
            .Named(nameof(DeleteExpiredStoredObjects))
            .WithExecution<DeleteExpiredStoredObjectsAction>()
            .RunContinuously()
                .Interval(TimeSpan.FromHours(1))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                )
            .Build();
        EndExpiredSessions = source.AddAction<EmptyRequest, EmptyActionResult>()
            .Named(nameof(EndExpiredSessions))
            .WithExecution<EndExpiredSessionsAction>()
            .RunContinuously()
                .Interval(TimeSpan.FromHours(1))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                )
            .Build();
        PurgeLogs = source.AddAction<EmptyRequest, EmptyActionResult>()
            .Named(nameof(PurgeLogs))
            .WithExecution<PurgeLogsAction>()
            .RunContinuously()
                .Interval(TimeSpan.FromHours(7))
                .AddSchedule
                (
                    Schedule.EveryDay().At(TimeRange.AllDay())
                )
            .Build();
    }

    public AppApiAction<EmptyRequest, EmptyActionResult> DeactivateUsers { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> DeleteExpiredStoredObjects { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> EndExpiredSessions { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> PurgeLogs { get; }
}