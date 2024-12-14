using XTI_HubWebAppApiActions.PermanentLog;

// Generated Code
namespace XTI_HubWebAppApi.PermanentLog;
public sealed partial class PermanentLogGroupBuilder
{
    private readonly AppApiGroup source;
    internal PermanentLogGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        LogBatch = source.AddAction<LogBatchModel, EmptyActionResult>("LogBatch").WithExecution<LogBatchAction>();
        LogSessionDetails = source.AddAction<LogSessionDetailsRequest, EmptyActionResult>("LogSessionDetails").WithExecution<LogSessionDetailsAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<LogBatchModel, EmptyActionResult> LogBatch { get; }
    public AppApiActionBuilder<LogSessionDetailsRequest, EmptyActionResult> LogSessionDetails { get; }

    public PermanentLogGroup Build() => new PermanentLogGroup(source, this);
}