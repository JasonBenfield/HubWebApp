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
        AddDeleteCommand = source.AddAction<InstallationIDRequest, AppDeleteCommandDetailModel>("AddDeleteCommand").WithExecution<AddDeleteCommandAction>().WithValidation<AddDeleteCommandValidation>();
        AddInstallCommand = source.AddAction<AddInstallCommandRequest, AppInstallCommandDetailModel>("AddInstallCommand").WithExecution<AddInstallCommandAction>().WithValidation<AddInstallCommandValidation>();
        BeginCommand = source.AddAction<AppCommandIDRequest, AppCommandModel>("BeginCommand").WithExecution<BeginCommandAction>().WithValidation<BeginCommandValidation>();
        BeginCommandStep = source.AddAction<BeginAppCommandStepRequest, AppCommandStepModel>("BeginCommandStep").WithExecution<BeginCommandStepAction>().WithValidation<BeginCommandStepValidation>();
        BeginDelete = source.AddAction<InstallationIDRequest, EmptyActionResult>("BeginDelete").WithExecution<BeginDeleteAction>().WithValidation<BeginDeleteValidation>();
        BeginInstallation = source.AddAction<BeginInstallationRequest, InstallationModel>("BeginInstallation").WithExecution<BeginInstallationAction>().WithValidation<BeginInstallationValidation>();
        CommandEnded = source.AddAction<AppCommandIDRequest, EmptyActionResult>("CommandEnded").WithExecution<CommandEndedAction>().WithValidation<CommandEndedValidation>();
        CommandStepEnded = source.AddAction<AppCommandStepEndedRequest, EmptyRequest>("CommandStepEnded").WithExecution<CommandStepEndedAction>().WithValidation<CommandStepEndedValidation>();
        Deleted = source.AddAction<InstallationIDRequest, EmptyActionResult>("Deleted").WithExecution<DeletedAction>().WithValidation<DeletedValidation>();
        GetDeleteCommandDetail = source.AddAction<AppCommandIDRequest, AppDeleteCommandDetailModel>("GetDeleteCommandDetail").WithExecution<GetDeleteCommandDetailAction>().WithValidation<GetDeleteCommandDetailValidation>();
        GetInstallationCommandDetail = source.AddAction<AppCommandIDRequest, AppInstallCommandDetailModel>("GetInstallationCommandDetail").WithExecution<GetInstallationCommandDetailAction>().WithValidation<GetInstallationCommandDetailValidation>();
        GetInstallationDetail = source.AddAction<InstallationIDRequest, InstallationDetailModel>("GetInstallationDetail").WithExecution<GetInstallationDetailAction>().WithValidation<GetInstallationDetailValidation>();
        GetPendingCommands = source.AddAction<GetPendingCommandsRequest, AppCommandModel[]>("GetPendingCommands").WithExecution<GetPendingCommandsAction>();
        Index = source.AddAction<InstallationQueryRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        Installation = source.AddAction<InstallationViewRequest, WebViewResult>("Installation").WithExecution<InstallationPage>();
        Installed = source.AddAction<InstallationIDRequest, EmptyActionResult>("Installed").WithExecution<InstalledAction>().WithValidation<InstalledValidation>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<InstallationIDRequest, AppDeleteCommandDetailModel> AddDeleteCommand { get; }
    public AppApiActionBuilder<AddInstallCommandRequest, AppInstallCommandDetailModel> AddInstallCommand { get; }
    public AppApiActionBuilder<AppCommandIDRequest, AppCommandModel> BeginCommand { get; }
    public AppApiActionBuilder<BeginAppCommandStepRequest, AppCommandStepModel> BeginCommandStep { get; }
    public AppApiActionBuilder<InstallationIDRequest, EmptyActionResult> BeginDelete { get; }
    public AppApiActionBuilder<BeginInstallationRequest, InstallationModel> BeginInstallation { get; }
    public AppApiActionBuilder<AppCommandIDRequest, EmptyActionResult> CommandEnded { get; }
    public AppApiActionBuilder<AppCommandStepEndedRequest, EmptyRequest> CommandStepEnded { get; }
    public AppApiActionBuilder<InstallationIDRequest, EmptyActionResult> Deleted { get; }
    public AppApiActionBuilder<AppCommandIDRequest, AppDeleteCommandDetailModel> GetDeleteCommandDetail { get; }
    public AppApiActionBuilder<AppCommandIDRequest, AppInstallCommandDetailModel> GetInstallationCommandDetail { get; }
    public AppApiActionBuilder<InstallationIDRequest, InstallationDetailModel> GetInstallationDetail { get; }
    public AppApiActionBuilder<GetPendingCommandsRequest, AppCommandModel[]> GetPendingCommands { get; }
    public AppApiActionBuilder<InstallationQueryRequest, WebViewResult> Index { get; }
    public AppApiActionBuilder<InstallationViewRequest, WebViewResult> Installation { get; }
    public AppApiActionBuilder<InstallationIDRequest, EmptyActionResult> Installed { get; }

    public InstallationsGroup Build() => new InstallationsGroup(source, this);
}