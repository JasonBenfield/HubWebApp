using XTI_HubWebAppApiActions.Periodic;

// Generated Code
namespace XTI_HubWebAppApi.Periodic;
public sealed partial class PeriodicGroup : AppApiGroupWrapper
{
    internal PeriodicGroup(AppApiGroup source, PeriodicGroupBuilder builder) : base(source)
    {
        DeactivateUsers = builder.DeactivateUsers.Build();
        DeleteExpiredStoredObjects = builder.DeleteExpiredStoredObjects.Build();
        EndExpiredSessions = builder.EndExpiredSessions.Build();
        PurgeLogs = builder.PurgeLogs.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<EmptyRequest, EmptyActionResult> DeactivateUsers { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> DeleteExpiredStoredObjects { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> EndExpiredSessions { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> PurgeLogs { get; }
}