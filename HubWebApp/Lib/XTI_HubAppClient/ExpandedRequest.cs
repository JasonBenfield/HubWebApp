// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ExpandedRequest
{
    public int RequestID { get; set; }

    public string Path { get; set; } = "";
    public string AppName { get; set; } = "";
    public string AppType { get; set; } = "";
    public string ResourceGroupName { get; set; } = "";
    public string ResourceName { get; set; } = "";
    public string ModCategoryName { get; set; } = "";
    public string ModKey { get; set; } = "";
    public string ModTargetKey { get; set; } = "";
    public string ModDisplayText { get; set; } = "";
    public int ActualCount { get; set; }

    public string UserName { get; set; } = "";
    public string UserGroupName { get; set; } = "";
    public DateTimeOffset TimeStarted { get; set; }

    public DateTimeOffset TimeEnded { get; set; }

    public TimeSpan? TimeElapsed { get; set; }

    public bool Succeeded { get; set; }

    public int CriticalErrorCount { get; set; }

    public string VersionName { get; set; } = "";
    public string VersionKey { get; set; } = "";
    public string VersionType { get; set; } = "";
    public string InstallLocation { get; set; } = "";
    public bool IsCurrentInstallation { get; set; }
}