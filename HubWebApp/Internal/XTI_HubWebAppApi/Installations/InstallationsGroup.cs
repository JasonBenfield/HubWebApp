namespace XTI_HubWebAppApi.Installations;

public sealed class InstallationsGroup : AppApiGroupWrapper
{
    public InstallationsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
        GetPendingDeletes = source.AddAction(nameof(GetPendingDeletes), () => sp.GetRequiredService<GetPendingDeletesAction>());
        RequestDelete = source.AddAction(nameof(RequestDelete), () => sp.GetRequiredService<RequestDeleteAction>());
        BeginDelete = source.AddAction(nameof(BeginDelete), () => sp.GetRequiredService<BeginDeleteAction>());
        Deleted = source.AddAction(nameof(Deleted), () => sp.GetRequiredService<DeletedAction>());
    }

    public AppApiAction<InstallationQueryRequest, WebViewResult> Index { get; }
    public AppApiAction<GetPendingDeletesRequest, InstallationModel[]> GetPendingDeletes { get; }
    public AppApiAction<GetInstallationRequest, EmptyActionResult> RequestDelete { get; }
    public AppApiAction<GetInstallationRequest, EmptyActionResult> BeginDelete { get; }
    public AppApiAction<GetInstallationRequest, EmptyActionResult> Deleted { get; }
}