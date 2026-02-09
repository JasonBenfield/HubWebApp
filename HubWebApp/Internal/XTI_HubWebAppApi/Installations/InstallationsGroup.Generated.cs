using XTI_HubWebAppApiActions.Installations;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Installations;
public sealed partial class InstallationsGroup : AppApiGroupWrapper
{
    internal InstallationsGroup(AppApiGroup source, InstallationsGroupBuilder builder) : base(source)
    {
        AddDeleteCommand = builder.AddDeleteCommand.Build();
        AddInstallCommand = builder.AddInstallCommand.Build();
        BeginCommand = builder.BeginCommand.Build();
        BeginCommandStep = builder.BeginCommandStep.Build();
        BeginDelete = builder.BeginDelete.Build();
        BeginInstallation = builder.BeginInstallation.Build();
        CommandEnded = builder.CommandEnded.Build();
        CommandStepEnded = builder.CommandStepEnded.Build();
        Deleted = builder.Deleted.Build();
        GetDeleteCommandDetail = builder.GetDeleteCommandDetail.Build();
        GetInstallationCommandDetail = builder.GetInstallationCommandDetail.Build();
        GetInstallationDetail = builder.GetInstallationDetail.Build();
        GetPendingCommands = builder.GetPendingCommands.Build();
        Index = builder.Index.Build();
        Installation = builder.Installation.Build();
        Installed = builder.Installed.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<InstallationIDRequest, AppDeleteCommandDetailModel> AddDeleteCommand { get; }
    public AppApiAction<AddInstallCommandRequest, AppInstallCommandDetailModel> AddInstallCommand { get; }
    public AppApiAction<AppCommandIDRequest, AppCommandModel> BeginCommand { get; }
    public AppApiAction<BeginAppCommandStepRequest, AppCommandStepModel> BeginCommandStep { get; }
    public AppApiAction<InstallationIDRequest, EmptyActionResult> BeginDelete { get; }
    public AppApiAction<BeginInstallationRequest, InstallationModel> BeginInstallation { get; }
    public AppApiAction<AppCommandIDRequest, EmptyActionResult> CommandEnded { get; }
    public AppApiAction<AppCommandStepEndedRequest, EmptyRequest> CommandStepEnded { get; }
    public AppApiAction<InstallationIDRequest, EmptyActionResult> Deleted { get; }
    public AppApiAction<AppCommandIDRequest, AppDeleteCommandDetailModel> GetDeleteCommandDetail { get; }
    public AppApiAction<AppCommandIDRequest, AppInstallCommandDetailModel> GetInstallationCommandDetail { get; }
    public AppApiAction<InstallationIDRequest, InstallationDetailModel> GetInstallationDetail { get; }
    public AppApiAction<GetPendingCommandsRequest, AppCommandModel[]> GetPendingCommands { get; }
    public AppApiAction<InstallationQueryRequest, WebViewResult> Index { get; }
    public AppApiAction<InstallationViewRequest, WebViewResult> Installation { get; }
    public AppApiAction<InstallationIDRequest, EmptyActionResult> Installed { get; }
}