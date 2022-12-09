namespace XTI_SupportServiceAppApi.Installations;

public sealed class InstallationsGroup : AppApiGroupWrapper
{
    public InstallationsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Delete = source.AddAction(nameof(Delete), () => sp.GetRequiredService<DeleteAction>());
    }

    public AppApiAction<EmptyRequest, EmptyActionResult> Delete { get; }
}