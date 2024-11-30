namespace XTI_SupportServiceAppApi.Installations;

public sealed class InstallationsGroup : AppApiGroupWrapper
{
    public InstallationsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Delete = source.AddAction<EmptyRequest, EmptyActionResult>()
            .Named(nameof(Delete))
            .WithExecution<DeleteAction>()
            .Build();
    }

    public AppApiAction<EmptyRequest, EmptyActionResult> Delete { get; }
}