using XTI_HubWebAppApiActions.Installations;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Installations;
public sealed partial class InstallationsGroup : AppApiGroupWrapper
{
    internal InstallationsGroup(AppApiGroup source, InstallationsGroupBuilder builder) : base(source)
    {
        BeginDelete = builder.BeginDelete.Build();
        Deleted = builder.Deleted.Build();
        GetInstallationActivities = builder.GetInstallationActivities.Build();
        GetInstallationDetail = builder.GetInstallationDetail.Build();
        Index = builder.Index.Build();
        Installation = builder.Installation.Build();
        RequestDelete = builder.RequestDelete.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<GetInstallationRequest, EmptyActionResult> BeginDelete { get; }
    public AppApiAction<GetInstallationRequest, EmptyActionResult> Deleted { get; }
    public AppApiAction<GetInstallationActivitiesRequest, InstallationActivitiesResult> GetInstallationActivities { get; }
    public AppApiAction<int, InstallationDetailModel> GetInstallationDetail { get; }
    public AppApiAction<InstallationQueryRequest, WebViewResult> Index { get; }
    public AppApiAction<InstallationViewRequest, WebViewResult> Installation { get; }
    public AppApiAction<GetInstallationRequest, EmptyActionResult> RequestDelete { get; }
}