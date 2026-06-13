using XTI_HubWebAppApiActions.Commands;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Commands;
public sealed partial class CommandsGroupBuilder
{
    private readonly AppApiGroup source;
    internal CommandsGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        GetCommandsInProgress = source.AddAction<EmptyRequest, AppCommandSummaryModel[]>("GetCommandsInProgress").WithExecution<GetCommandsInProgressAction>();
        GetPendingCommands = source.AddAction<GetPendingCommandsRequest, AppCommandSummaryModel[]>("GetPendingCommands").WithExecution<GetPendingCommandsAction>();
        Index = source.AddAction<AppCommandIDRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<EmptyRequest, AppCommandSummaryModel[]> GetCommandsInProgress { get; }
    public AppApiActionBuilder<GetPendingCommandsRequest, AppCommandSummaryModel[]> GetPendingCommands { get; }
    public AppApiActionBuilder<AppCommandIDRequest, WebViewResult> Index { get; }

    public CommandsGroup Build() => new CommandsGroup(source, this);
}