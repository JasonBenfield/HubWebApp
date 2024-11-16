namespace XTI_SupportServiceAppApi.PermanentLog;

public sealed class PermanentLogGroup : AppApiGroupWrapper
{
    public PermanentLogGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        MoveToPermanent = source.AddAction
        (
            nameof(MoveToPermanent), () => sp.GetRequiredService<MoveToPermanentAction>()
        );
        MoveToPermanentV1 = source.AddAction
        (
            nameof(MoveToPermanentV1), () => sp.GetRequiredService<MoveToPermanentV1Action>()
        );
        Retry = source.AddAction
        (
            nameof(Retry), () => sp.GetRequiredService<RetryAction>()
        );
        RetryV1 = source.AddAction
        (
            nameof(RetryV1), () => sp.GetRequiredService<RetryV1Action>()
        );
    }

    public AppApiAction<EmptyRequest, EmptyActionResult> MoveToPermanent { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> MoveToPermanentV1 { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> Retry { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> RetryV1 { get; }
}