// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ExpandedInstallation
{
    public int InstallationID { get; set; }

    public bool IsCurrent { get; set; }

    public string InstallationStatusDisplayText { get; set; } = "";
    public DateTimeOffset TimeInstallationAdded { get; set; }

    public string QualifiedMachineName { get; set; } = "";
    public string Domain { get; set; } = "";
    public int AppID { get; set; }

    public string AppKey { get; set; } = "";
    public string AppName { get; set; } = "";
    public string AppTypeDisplayText { get; set; } = "";
    public string VersionName { get; set; } = "";
    public string VersionRelease { get; set; } = "";
    public string VersionKey { get; set; } = "";
    public string VersionStatusText { get; set; } = "";
    public string VersionTypeText { get; set; } = "";
    public DateTimeOffset? LastRequestTime { get; set; }

    public int? LastRequestDaysAgo { get; set; }

    public int RequestCount { get; set; }
}