using XTI_TempLog.Abstractions;

namespace XTI_HubWebAppApi.PermanentLog;

public sealed class PermanentLogGroup : AppApiGroupWrapper
{
    public PermanentLogGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        LogBatch = source.AddAction(nameof(LogBatch), () => sp.GetRequiredService<LogBatchAction>());
        EndExpiredSessions = source.AddAction(nameof(EndExpiredSessions), () => sp.GetRequiredService<EndExpiredSessionsAction>());
    }

    public AppApiAction<LogBatchModel, EmptyActionResult> LogBatch { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> EndExpiredSessions { get; }
}