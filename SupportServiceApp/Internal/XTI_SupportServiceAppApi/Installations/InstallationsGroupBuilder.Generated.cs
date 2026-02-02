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
        ExecuteInstallationActivities = source.AddAction<EmptyRequest, EmptyActionResult>("ExecuteInstallationActivities").WithExecution<ExecuteInstallationActivitiesAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> ExecuteInstallationActivities { get; }

    public InstallationsGroup Build() => new InstallationsGroup(source, this);
}