namespace XTI_HubWebAppApi.Installations;

public sealed class InstallationsGroup : AppApiGroupWrapper
{
    public InstallationsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
        Installation = source.AddAction(nameof(Installation), () => sp.GetRequiredService<InstallationViewAction>());
        GetInstallationDetail = source.AddAction(nameof(GetInstallationDetail), () => sp.GetRequiredService<GetInstallationDetailAction>());
        GetPendingDeletes = source.AddAction
        (
            nameof(GetPendingDeletes),
            () => sp.GetRequiredService<GetPendingDeletesAction>(),
            access: Access.WithAllowed(HubInfo.Roles.InstallationManager)
        );
        RequestDelete = source.AddAction(nameof(RequestDelete), () => sp.GetRequiredService<RequestDeleteAction>());
        BeginDelete = source.AddAction(nameof(BeginDelete), () => sp.GetRequiredService<BeginDeleteAction>());
        Deleted = source.AddAction(nameof(Deleted), () => sp.GetRequiredService<DeletedAction>());
    }

    public AppApiAction<InstallationQueryRequest, WebViewResult> Index { get; }
    public AppApiAction<InstallationViewRequest, WebViewResult> Installation { get; }
    public AppApiAction<int, InstallationDetailModel> GetInstallationDetail { get; }
    public AppApiAction<GetPendingDeletesRequest, AppVersionInstallationModel[]> GetPendingDeletes { get; }
    public AppApiAction<GetInstallationRequest, EmptyActionResult> RequestDelete { get; }
    public AppApiAction<GetInstallationRequest, EmptyActionResult> BeginDelete { get; }
    public AppApiAction<GetInstallationRequest, EmptyActionResult> Deleted { get; }
}