using XTI_HubWebAppApiActions.Periodic;

// Generated Code
namespace XTI_HubWebAppApi.Periodic;
public sealed partial class PeriodicGroupBuilder
{
    private readonly AppApiGroup source;
    internal PeriodicGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        DeactivateUsers = source.AddAction<EmptyRequest, EmptyActionResult>("DeactivateUsers").WithExecution<DeactivateUsersAction>();
        DeleteExpiredStoredObjects = source.AddAction<EmptyRequest, EmptyActionResult>("DeleteExpiredStoredObjects").WithExecution<DeleteExpiredStoredObjectsAction>();
        EndExpiredSessions = source.AddAction<EmptyRequest, EmptyActionResult>("EndExpiredSessions").WithExecution<EndExpiredSessionsAction>();
        PurgeLogs = source.AddAction<EmptyRequest, EmptyActionResult>("PurgeLogs").WithExecution<PurgeLogsAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> DeactivateUsers { get; }
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> DeleteExpiredStoredObjects { get; }
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> EndExpiredSessions { get; }
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> PurgeLogs { get; }

    public PeriodicGroup Build() => new PeriodicGroup(source, this);
}