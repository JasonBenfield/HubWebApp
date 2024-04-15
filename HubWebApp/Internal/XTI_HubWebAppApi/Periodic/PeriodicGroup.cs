namespace XTI_HubWebAppApi.Periodic;

public sealed class PeriodicGroup : AppApiGroupWrapper
{
    public PeriodicGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        DeactivateUsers = source.AddAction(nameof(DeactivateUsers), () => sp.GetRequiredService<DeactivateUsersAction>());
        DeleteExpiredStoredObjects = source.AddAction(nameof(DeleteExpiredStoredObjects), () => sp.GetRequiredService<DeleteExpiredStoredObjectsAction>());
        EndExpiredSessions = source.AddAction(nameof(EndExpiredSessions), () => sp.GetRequiredService<EndExpiredSessionsAction>());
        PurgeLogs = source.AddAction(nameof(PurgeLogs), () => sp.GetRequiredService<PurgeLogsAction>());
    }

    public AppApiAction<EmptyRequest, EmptyActionResult> DeactivateUsers { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> DeleteExpiredStoredObjects { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> EndExpiredSessions { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> PurgeLogs { get; }
}