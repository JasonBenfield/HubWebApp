using XTI_TempLog.Abstractions;

namespace XTI_HubWebAppApi.PermanentLog;

public sealed class PermanentLogGroup : AppApiGroupWrapper
{
    public PermanentLogGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        LogBatch = source.AddAction<LogBatchModel, EmptyActionResult>(nameof(LogBatch))
            .WithExecution<LogBatchAction>()
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(5).Minutes()
            .Build();
        LogSessionDetails = source.AddAction<LogSessionDetailsRequest, EmptyActionResult>(nameof(LogSessionDetails))
            .WithExecution<LogSessionDetailsAction>()
            .ThrottleRequestLogging().ForOneHour()
            .ThrottleExceptionLogging().For(5).Minutes()
            .Build();
    }

    public AppApiAction<LogBatchModel, EmptyActionResult> LogBatch { get; }
    public AppApiAction<LogSessionDetailsRequest, EmptyActionResult> LogSessionDetails { get; }
}