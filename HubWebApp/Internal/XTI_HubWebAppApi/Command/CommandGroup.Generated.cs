using XTI_HubWebAppApiActions.Command;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Command;
public sealed partial class CommandGroup : AppApiGroupWrapper
{
    internal CommandGroup(AppApiGroup source, CommandGroupBuilder builder) : base(source)
    {
        BeginCommand = builder.BeginCommand.Build();
        BeginCommandStep = builder.BeginCommandStep.Build();
        BeginInstallation = builder.BeginInstallation.Build();
        CommandEnded = builder.CommandEnded.Build();
        CommandStepEnded = builder.CommandStepEnded.Build();
        GetCommand = builder.GetCommand.Build();
        GetDeleteCommandDetail = builder.GetDeleteCommandDetail.Build();
        GetInstallationCommandDetail = builder.GetInstallationCommandDetail.Build();
        Index = builder.Index.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<AppCommandIDRequest, AppCommandModel> BeginCommand { get; }
    public AppApiAction<BeginAppCommandStepRequest, AppCommandStepModel> BeginCommandStep { get; }
    public AppApiAction<BeginInstallationRequest, InstallationModel> BeginInstallation { get; }
    public AppApiAction<AppCommandIDRequest, EmptyActionResult> CommandEnded { get; }
    public AppApiAction<AppCommandStepEndedRequest, EmptyRequest> CommandStepEnded { get; }
    public AppApiAction<AppCommandIDRequest, AppCommandModel> GetCommand { get; }
    public AppApiAction<AppCommandIDRequest, AppDeleteCommandDetailModel> GetDeleteCommandDetail { get; }
    public AppApiAction<AppCommandIDRequest, AppInstallCommandDetailModel> GetInstallationCommandDetail { get; }
    public AppApiAction<AppCommandIDRequest, WebViewResult> Index { get; }
}