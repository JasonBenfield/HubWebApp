using XTI_HubWebAppApi.Periodic;
using XTI_TempLog.Abstractions;

namespace XTI_HubWebAppApi.PermanentLog;

public sealed class PermanentLogGroup : AppApiGroupWrapper
{
    public PermanentLogGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        LogBatch = source.AddAction(nameof(LogBatch), () => sp.GetRequiredService<LogBatchAction>());
        LogSessionDetails = source.AddAction(nameof(LogSessionDetails), () => sp.GetRequiredService<LogSessionDetailsAction>());
    }

    public AppApiAction<LogBatchModel, EmptyActionResult> LogBatch { get; }
    public AppApiAction<LogSessionDetailsRequest, EmptyActionResult> LogSessionDetails { get; }
}