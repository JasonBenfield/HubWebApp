namespace XTI_SupportServiceAppApi.Installations;

public sealed class InstallationsGroup : AppApiGroupWrapper
{
    public InstallationsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        DoSomething = source.AddAction(nameof(DoSomething), () => sp.GetRequiredService<DeleteAction>());
    }

    public AppApiAction<EmptyRequest, EmptyActionResult> DoSomething { get; }
}