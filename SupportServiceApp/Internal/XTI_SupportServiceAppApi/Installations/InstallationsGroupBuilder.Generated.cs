using XTI_SupportServiceAppApi.Installations;

// Generated Code
#nullable enable
namespace XTI_SupportServiceAppApi.Installations;
public sealed partial class InstallationsGroupBuilder
{
    private readonly AppApiGroup source;
    internal InstallationsGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        Delete = source.AddAction<EmptyRequest, EmptyActionResult>("Delete").WithExecution<DeleteAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> Delete { get; }

    public InstallationsGroup Build() => new InstallationsGroup(source, this);
}