using XTI_HubWebAppApiActions.PermanentLog;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.PermanentLog;
public sealed partial class PermanentLogGroupBuilder
{
    private readonly AppApiGroup source;
    internal PermanentLogGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        LogSessionDetails = source.AddAction<LogSessionDetailsRequest, EmptyActionResult>("LogSessionDetails").WithExecution<LogSessionDetailsAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<LogSessionDetailsRequest, EmptyActionResult> LogSessionDetails { get; }

    public PermanentLogGroup Build() => new PermanentLogGroup(source, this);
}