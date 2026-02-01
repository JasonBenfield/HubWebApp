using XTI_SupportServiceAppApi.PermanentLog;

// Generated Code
#nullable enable
namespace XTI_SupportServiceAppApi.PermanentLog;
public sealed partial class PermanentLogGroup : AppApiGroupWrapper
{
    internal PermanentLogGroup(AppApiGroup source, PermanentLogGroupBuilder builder) : base(source)
    {
        MoveToPermanent = builder.MoveToPermanent.Build();
        Retry = builder.Retry.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<EmptyRequest, EmptyActionResult> MoveToPermanent { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> Retry { get; }
}