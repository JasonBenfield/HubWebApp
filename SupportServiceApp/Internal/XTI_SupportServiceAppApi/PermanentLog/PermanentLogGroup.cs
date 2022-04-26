namespace XTI_SupportServiceAppApi.PermanentLog;

public sealed class PermanentLogGroup : AppApiGroupWrapper
{
    public PermanentLogGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new AppApiActionFactory(source);
        MoveToPermanent = source.AddAction
        (
            actions.Action(nameof(MoveToPermanent), () => sp.GetRequiredService<MoveToPermanentAction>())
        );
        Retry = source.AddAction
        (
            actions.Action(nameof(Retry), () => sp.GetRequiredService<RetryAction>())
        );
    }

    public AppApiAction<EmptyRequest, EmptyActionResult> MoveToPermanent { get; }
    public AppApiAction<EmptyRequest, EmptyActionResult> Retry { get; }
}