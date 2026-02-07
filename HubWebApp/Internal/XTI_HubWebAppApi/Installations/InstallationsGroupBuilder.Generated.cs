using XTI_HubWebAppApiActions.Installations;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Installations;
public sealed partial class InstallationsGroupBuilder
{
    private readonly AppApiGroup source;
    internal InstallationsGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        BeginDelete = source.AddAction<InstallationIDRequest, EmptyActionResult>("BeginDelete").WithExecution<BeginDeleteAction>().WithValidation<BeginDeleteValidation>();
        BeginInstallation = source.AddAction<BeginInstallationRequest, InstallationModel>("BeginInstallation").WithExecution<BeginInstallationAction>().WithValidation<BeginInstallationValidation>();
        BeginRequestedInstallation = source.AddAction<AppCommandIDRequest, AppCommandModel>("BeginRequestedInstallation").WithExecution<BeginRequestedInstallationAction>().WithValidation<BeginRequestedInstallationValidation>();
        BeginRequestedInstallationStep = source.AddAction<BeginAppCommandStepRequest, AppCommandStepModel>("BeginRequestedInstallationStep").WithExecution<BeginRequestedInstallationStepAction>();
        Deleted = source.AddAction<InstallationIDRequest, EmptyActionResult>("Deleted").WithExecution<DeletedAction>().WithValidation<DeletedValidation>();
        GetInstallationActivities = source.AddAction<GetInstallationActivitiesRequest, InstallationActivitiesResult>("GetInstallationActivities").WithExecution<GetInstallationActivitiesAction>();
        GetInstallationDetail = source.AddAction<InstallationIDRequest, InstallationDetailModel>("GetInstallationDetail").WithExecution<GetInstallationDetailAction>().WithValidation<GetInstallationDetailValidation>();
        GetRequestedInstallationDetail = source.AddAction<AppCommandIDRequest, AppInstallCommandDetailModel>("GetRequestedInstallationDetail").WithExecution<GetRequestedInstallationDetailAction>().WithValidation<GetRequestedInstallationDetailValidation>();
        Index = source.AddAction<InstallationQueryRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        Installation = source.AddAction<InstallationViewRequest, WebViewResult>("Installation").WithExecution<InstallationPage>();
        Installed = source.AddAction<InstallationIDRequest, EmptyActionResult>("Installed").WithExecution<InstalledAction>().WithValidation<InstalledValidation>();
        RequestDelete = source.AddAction<InstallationIDRequest, EmptyActionResult>("RequestDelete").WithExecution<RequestDeleteAction>().WithValidation<RequestDeleteValidation>();
        RequestedInstallationEnded = source.AddAction<AppCommandIDRequest, EmptyActionResult>("RequestedInstallationEnded").WithExecution<RequestedInstallationEndedAction>().WithValidation<RequestedInstallationEndedValidation>();
        RequestedInstallationStepEnded = source.AddAction<AppCommandStepEndedRequest, EmptyRequest>("RequestedInstallationStepEnded").WithExecution<RequestedInstallationStepEndedAction>().WithValidation<RequestedInstallationStepEndedValidation>();
        RequestInstallation = source.AddAction<AddAppInstallCommandRequest, AppInstallCommandDetailModel>("RequestInstallation").WithExecution<RequestInstallationAction>().WithValidation<RequestInstallationValidation>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<InstallationIDRequest, EmptyActionResult> BeginDelete { get; }
    public AppApiActionBuilder<BeginInstallationRequest, InstallationModel> BeginInstallation { get; }
    public AppApiActionBuilder<AppCommandIDRequest, AppCommandModel> BeginRequestedInstallation { get; }
    public AppApiActionBuilder<BeginAppCommandStepRequest, AppCommandStepModel> BeginRequestedInstallationStep { get; }
    public AppApiActionBuilder<InstallationIDRequest, EmptyActionResult> Deleted { get; }
    public AppApiActionBuilder<GetInstallationActivitiesRequest, InstallationActivitiesResult> GetInstallationActivities { get; }
    public AppApiActionBuilder<InstallationIDRequest, InstallationDetailModel> GetInstallationDetail { get; }
    public AppApiActionBuilder<AppCommandIDRequest, AppInstallCommandDetailModel> GetRequestedInstallationDetail { get; }
    public AppApiActionBuilder<InstallationQueryRequest, WebViewResult> Index { get; }
    public AppApiActionBuilder<InstallationViewRequest, WebViewResult> Installation { get; }
    public AppApiActionBuilder<InstallationIDRequest, EmptyActionResult> Installed { get; }
    public AppApiActionBuilder<InstallationIDRequest, EmptyActionResult> RequestDelete { get; }
    public AppApiActionBuilder<AppCommandIDRequest, EmptyActionResult> RequestedInstallationEnded { get; }
    public AppApiActionBuilder<AppCommandStepEndedRequest, EmptyRequest> RequestedInstallationStepEnded { get; }
    public AppApiActionBuilder<AddAppInstallCommandRequest, AppInstallCommandDetailModel> RequestInstallation { get; }

    public InstallationsGroup Build() => new InstallationsGroup(source, this);
}