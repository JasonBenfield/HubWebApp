using XTI_SupportServiceAppApi.PermanentLog;

// Generated Code
#nullable enable
namespace XTI_SupportServiceAppApi.PermanentLog;
public sealed partial class PermanentLogGroupBuilder
{
    private readonly AppApiGroup source;
    internal PermanentLogGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        MoveToPermanent = source.AddAction<EmptyRequest, EmptyActionResult>("MoveToPermanent").WithExecution<MoveToPermanentAction>();
        MoveToPermanentV1 = source.AddAction<EmptyRequest, EmptyActionResult>("MoveToPermanentV1").WithExecution<MoveToPermanentV1Action>();
        Retry = source.AddAction<EmptyRequest, EmptyActionResult>("Retry").WithExecution<RetryAction>();
        RetryV1 = source.AddAction<EmptyRequest, EmptyActionResult>("RetryV1").WithExecution<RetryV1Action>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> MoveToPermanent { get; }
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> MoveToPermanentV1 { get; }
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> Retry { get; }
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> RetryV1 { get; }

    public PermanentLogGroup Build() => new PermanentLogGroup(source, this);
}