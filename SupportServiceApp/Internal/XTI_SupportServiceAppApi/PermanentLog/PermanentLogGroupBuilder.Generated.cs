using XTI_SupportServiceAppApiActions.PermanentLog;

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
        Retry = source.AddAction<EmptyRequest, EmptyActionResult>("Retry").WithExecution<RetryAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> MoveToPermanent { get; }
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> Retry { get; }

    public PermanentLogGroup Build() => new PermanentLogGroup(source, this);
}