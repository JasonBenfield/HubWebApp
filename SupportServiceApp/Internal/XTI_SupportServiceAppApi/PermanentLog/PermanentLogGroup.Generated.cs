using XTI_SupportServiceAppApi.PermanentLog;

// Generated Code
#nullable enable
namespace XTI_SupportServiceAppApi.PermanentLog;
public sealed partial class PermanentLogGroup : AppApiGroupWrapper
{
    internal PermanentLogGroup(AppApiGroup source, PermanentLogGroupBuilder builder) : base(source)
    {
        MoveToPermanent = builder.MoveToPermanent.Build();
        MoveToPermanentV1 = builder.MoveToPermanentV1.Build();
        Retry = builder.Retry.Build();
        RetryV1 = builder.RetryV1.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<EmptyRequest, EmptyActionResult> MoveToPermanent { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> MoveToPermanentV1 { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> Retry { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> RetryV1 { get; }
}