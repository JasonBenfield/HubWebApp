namespace XTI_HubWebAppApi.Periodic;

public sealed class PeriodicGroup : AppApiGroupWrapper
{
    public PeriodicGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        PurgeLogs = source.AddAction(nameof(PurgeLogs), () => sp.GetRequiredService<PurgeLogsAction>());
    }

    public AppApiAction<EmptyRequest, EmptyActionResult> PurgeLogs { get; }
}