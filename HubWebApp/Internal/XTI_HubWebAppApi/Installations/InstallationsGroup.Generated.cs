using XTI_HubWebAppApiActions.Installations;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Installations;
public sealed partial class InstallationsGroup : AppApiGroupWrapper
{
    internal InstallationsGroup(AppApiGroup source, InstallationsGroupBuilder builder) : base(source)
    {
        BeginDelete = builder.BeginDelete.Build();
        BeginInstallation = builder.BeginInstallation.Build();
        BeginRequestedInstallation = builder.BeginRequestedInstallation.Build();
        BeginRequestedInstallationStep = builder.BeginRequestedInstallationStep.Build();
        Deleted = builder.Deleted.Build();
        GetInstallationActivities = builder.GetInstallationActivities.Build();
        GetInstallationDetail = builder.GetInstallationDetail.Build();
        GetRequestedInstallationDetail = builder.GetRequestedInstallationDetail.Build();
        Index = builder.Index.Build();
        Installation = builder.Installation.Build();
        Installed = builder.Installed.Build();
        RequestDelete = builder.RequestDelete.Build();
        RequestedInstallationEnded = builder.RequestedInstallationEnded.Build();
        RequestedInstallationStepEnded = builder.RequestedInstallationStepEnded.Build();
        RequestInstallation = builder.RequestInstallation.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<InstallationIDRequest, EmptyActionResult> BeginDelete { get; }
    public AppApiAction<BeginInstallationRequest, InstallationModel> BeginInstallation { get; }
    public AppApiAction<AppCommandIDRequest, AppCommandModel> BeginRequestedInstallation { get; }
    public AppApiAction<BeginAppCommandStepRequest, AppCommandStepModel> BeginRequestedInstallationStep { get; }
    public AppApiAction<InstallationIDRequest, EmptyActionResult> Deleted { get; }
    public AppApiAction<GetInstallationActivitiesRequest, InstallationActivitiesResult> GetInstallationActivities { get; }
    public AppApiAction<InstallationIDRequest, InstallationDetailModel> GetInstallationDetail { get; }
    public AppApiAction<AppCommandIDRequest, AppInstallCommandDetailModel> GetRequestedInstallationDetail { get; }
    public AppApiAction<InstallationQueryRequest, WebViewResult> Index { get; }
    public AppApiAction<InstallationViewRequest, WebViewResult> Installation { get; }
    public AppApiAction<InstallationIDRequest, EmptyActionResult> Installed { get; }
    public AppApiAction<InstallationIDRequest, EmptyActionResult> RequestDelete { get; }
    public AppApiAction<AppCommandIDRequest, EmptyActionResult> RequestedInstallationEnded { get; }
    public AppApiAction<AppCommandStepEndedRequest, EmptyRequest> RequestedInstallationStepEnded { get; }
    public AppApiAction<AddAppInstallCommandRequest, AppInstallCommandDetailModel> RequestInstallation { get; }
}