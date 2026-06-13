using XTI_HubWebAppApiActions.Command;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Command;
public sealed partial class CommandGroupBuilder
{
    private readonly AppApiGroup source;
    internal CommandGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        BeginCommand = source.AddAction<AppCommandIDRequest, AppCommandModel>("BeginCommand").WithExecution<BeginCommandAction>().WithValidation<BeginCommandValidation>();
        BeginCommandStep = source.AddAction<BeginAppCommandStepRequest, AppCommandStepModel>("BeginCommandStep").WithExecution<BeginCommandStepAction>().WithValidation<BeginCommandStepValidation>();
        BeginInstallation = source.AddAction<BeginInstallationRequest, InstallationModel>("BeginInstallation").WithExecution<BeginInstallationAction>().WithValidation<BeginInstallationValidation>();
        CommandEnded = source.AddAction<AppCommandIDRequest, EmptyActionResult>("CommandEnded").WithExecution<CommandEndedAction>().WithValidation<CommandEndedValidation>();
        CommandStepEnded = source.AddAction<AppCommandStepEndedRequest, EmptyRequest>("CommandStepEnded").WithExecution<CommandStepEndedAction>().WithValidation<CommandStepEndedValidation>();
        GetCommand = source.AddAction<AppCommandIDRequest, AppCommandModel>("GetCommand").WithExecution<GetCommandAction>().WithValidation<GetCommandValidation>();
        GetDeleteCommandDetail = source.AddAction<AppCommandIDRequest, AppDeleteCommandDetailModel>("GetDeleteCommandDetail").WithExecution<GetDeleteCommandDetailAction>().WithValidation<GetDeleteCommandDetailValidation>();
        GetInstallationCommandDetail = source.AddAction<AppCommandIDRequest, AppInstallCommandDetailModel>("GetInstallationCommandDetail").WithExecution<GetInstallationCommandDetailAction>().WithValidation<GetInstallationCommandDetailValidation>();
        Index = source.AddAction<AppCommandIDRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<AppCommandIDRequest, AppCommandModel> BeginCommand { get; }
    public AppApiActionBuilder<BeginAppCommandStepRequest, AppCommandStepModel> BeginCommandStep { get; }
    public AppApiActionBuilder<BeginInstallationRequest, InstallationModel> BeginInstallation { get; }
    public AppApiActionBuilder<AppCommandIDRequest, EmptyActionResult> CommandEnded { get; }
    public AppApiActionBuilder<AppCommandStepEndedRequest, EmptyRequest> CommandStepEnded { get; }
    public AppApiActionBuilder<AppCommandIDRequest, AppCommandModel> GetCommand { get; }
    public AppApiActionBuilder<AppCommandIDRequest, AppDeleteCommandDetailModel> GetDeleteCommandDetail { get; }
    public AppApiActionBuilder<AppCommandIDRequest, AppInstallCommandDetailModel> GetInstallationCommandDetail { get; }
    public AppApiActionBuilder<AppCommandIDRequest, WebViewResult> Index { get; }

    public CommandGroup Build() => new CommandGroup(source, this);
}