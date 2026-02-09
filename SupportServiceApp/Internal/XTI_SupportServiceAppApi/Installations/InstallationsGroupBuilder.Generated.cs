using XTI_SupportServiceAppApiActions.Installations;

// Generated Code
#nullable enable
namespace XTI_SupportServiceAppApi.Installations;
public sealed partial class InstallationsGroupBuilder
{
    private readonly AppApiGroup source;
    internal InstallationsGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        ExecutePendingCommands = source.AddAction<EmptyRequest, EmptyActionResult>("ExecutePendingCommands").WithExecution<ExecutePendingCommandsAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> ExecutePendingCommands { get; }

    public InstallationsGroup Build() => new InstallationsGroup(source, this);
}