using XTI_HubWebAppApiActions.Installations;

// Generated Code
namespace XTI_HubWebAppApi.Installations;
public sealed partial class InstallationsGroupBuilder
{
    private readonly AppApiGroup source;
    internal InstallationsGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        BeginDelete = source.AddAction<GetInstallationRequest, EmptyActionResult>("BeginDelete").WithExecution<BeginDeleteAction>();
        Deleted = source.AddAction<GetInstallationRequest, EmptyActionResult>("Deleted").WithExecution<DeletedAction>();
        GetInstallationDetail = source.AddAction<int, InstallationDetailModel>("GetInstallationDetail").WithExecution<GetInstallationDetailAction>();
        GetPendingDeletes = source.AddAction<GetPendingDeletesRequest, AppVersionInstallationModel[]>("GetPendingDeletes").WithExecution<GetPendingDeletesAction>();
        Index = source.AddAction<InstallationQueryRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        InstallationView = source.AddAction<InstallationViewRequest, WebViewResult>("InstallationView").WithExecution<InstallationViewAction>();
        RequestDelete = source.AddAction<GetInstallationRequest, EmptyActionResult>("RequestDelete").WithExecution<RequestDeleteAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<GetInstallationRequest, EmptyActionResult> BeginDelete { get; }
    public AppApiActionBuilder<GetInstallationRequest, EmptyActionResult> Deleted { get; }
    public AppApiActionBuilder<int, InstallationDetailModel> GetInstallationDetail { get; }
    public AppApiActionBuilder<GetPendingDeletesRequest, AppVersionInstallationModel[]> GetPendingDeletes { get; }
    public AppApiActionBuilder<InstallationQueryRequest, WebViewResult> Index { get; }
    public AppApiActionBuilder<InstallationViewRequest, WebViewResult> InstallationView { get; }
    public AppApiActionBuilder<GetInstallationRequest, EmptyActionResult> RequestDelete { get; }

    public InstallationsGroup Build() => new InstallationsGroup(source, this);
}