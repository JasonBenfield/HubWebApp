using XTI_HubWebAppApiActions.Command;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Command;
public sealed partial class CommandGroup : AppApiGroupWrapper
{
    internal CommandGroup(AppApiGroup source, CommandGroupBuilder builder) : base(source)
    {
        Index = builder.Index.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<AppCommandIDRequest, WebViewResult> Index { get; }
}