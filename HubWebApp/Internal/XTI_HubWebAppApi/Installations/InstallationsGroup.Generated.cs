using XTI_HubWebAppApiActions.Installations;

// Generated Code
namespace XTI_HubWebAppApi.Installations;
public sealed partial class InstallationsGroup : AppApiGroupWrapper
{
    internal InstallationsGroup(AppApiGroup source, InstallationsGroupBuilder builder) : base(source)
    {
        BeginDelete = builder.BeginDelete.Build();
        Deleted = builder.Deleted.Build();
        GetInstallationDetail = builder.GetInstallationDetail.Build();
        GetPendingDeletes = builder.GetPendingDeletes.Build();
        Index = builder.Index.Build();
        InstallationView = builder.InstallationView.Build();
        RequestDelete = builder.RequestDelete.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<GetInstallationRequest, EmptyActionResult> BeginDelete { get; }
    public AppApiAction<GetInstallationRequest, EmptyActionResult> Deleted { get; }
    public AppApiAction<int, InstallationDetailModel> GetInstallationDetail { get; }
    public AppApiAction<GetPendingDeletesRequest, AppVersionInstallationModel[]> GetPendingDeletes { get; }
    public AppApiAction<InstallationQueryRequest, WebViewResult> Index { get; }
    public AppApiAction<InstallationViewRequest, WebViewResult> InstallationView { get; }
    public AppApiAction<GetInstallationRequest, EmptyActionResult> RequestDelete { get; }
}