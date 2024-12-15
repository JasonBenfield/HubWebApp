using XTI_HubWebAppApiActions.PermanentLog;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.PermanentLog;
public sealed partial class PermanentLogGroup : AppApiGroupWrapper
{
    internal PermanentLogGroup(AppApiGroup source, PermanentLogGroupBuilder builder) : base(source)
    {
        LogBatch = builder.LogBatch.Build();
        LogSessionDetails = builder.LogSessionDetails.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<LogBatchModel, EmptyActionResult> LogBatch { get; }
    public AppApiAction<LogSessionDetailsRequest, EmptyActionResult> LogSessionDetails { get; }
}