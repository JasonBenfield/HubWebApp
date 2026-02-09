using XTI_SupportServiceAppApiActions.Installations;

// Generated Code
#nullable enable
namespace XTI_SupportServiceAppApi.Installations;
public sealed partial class InstallationsGroup : AppApiGroupWrapper
{
    internal InstallationsGroup(AppApiGroup source, InstallationsGroupBuilder builder) : base(source)
    {
        ExecutePendingCommands = builder.ExecutePendingCommands.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<EmptyRequest, EmptyActionResult> ExecutePendingCommands { get; }
}