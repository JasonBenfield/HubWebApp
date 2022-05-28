using XTI_TempLog.Abstractions;

namespace XTI_HubAppApi.PermanentLog;

public sealed class PermanentLogGroup : AppApiGroupWrapper
{
    public PermanentLogGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        LogBatch = source.AddAction(actions.Action(nameof(LogBatch), () => sp.GetRequiredService<LogBatchAction>()));
        EndExpiredSessions = source.AddAction(actions.Action(nameof(EndExpiredSessions), () => sp.GetRequiredService<EndExpiredSessionsAction>()));
    }

    public AppApiAction<LogBatchModel, EmptyActionResult> LogBatch { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> EndExpiredSessions { get; }
}