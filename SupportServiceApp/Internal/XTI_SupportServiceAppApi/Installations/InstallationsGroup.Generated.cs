using XTI_SupportServiceAppApi.Installations;

// Generated Code
#nullable enable
namespace XTI_SupportServiceAppApi.Installations;
public sealed partial class InstallationsGroup : AppApiGroupWrapper
{
    internal InstallationsGroup(AppApiGroup source, InstallationsGroupBuilder builder) : base(source)
    {
        ExecuteInstallationActivities = builder.ExecuteInstallationActivities.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<EmptyRequest, EmptyActionResult> ExecuteInstallationActivities { get; }
}