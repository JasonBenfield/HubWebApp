using XTI_HubWebAppApiActions.Commands;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Commands;
public sealed partial class CommandsGroup : AppApiGroupWrapper
{
    internal CommandsGroup(AppApiGroup source, CommandsGroupBuilder builder) : base(source)
    {
        GetCommandsInProgress = builder.GetCommandsInProgress.Build();
        Index = builder.Index.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<EmptyRequest, AppCommandSummaryModel[]> GetCommandsInProgress { get; }
    public AppApiAction<AppCommandIDRequest, WebViewResult> Index { get; }
}