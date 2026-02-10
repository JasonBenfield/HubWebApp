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
        Index = source.AddAction<AppCommandIDRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<AppCommandIDRequest, WebViewResult> Index { get; }

    public CommandGroup Build() => new CommandGroup(source, this);
}