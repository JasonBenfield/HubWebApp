namespace XTI_HubDB.Entities;

public sealed class ExpandedRequest
{
    public int RequestID { get; set; }
    public string Path { get; set; } = "";
    public int AppID { get; set; }
    public string AppKey { get; set; } = "";
    public string AppName { get; set; } = "";
    public string AppTypeText { get; set; } = "";
    public string ResourceGroupName { get; set; } = "";
    public string ResourceName { get; set; } = "";
    public string ModCategoryName { get; set; } = "";
    public string ModKey { get; set; } = "";
    public string ModTargetKey { get; set; } = "";
    public string ModDisplayText { get; set; } = "";
    public int ActualCount { get; set; }
    public int SessionID { get; set; }
    public string UserName { get; set; } = "";
    public int UserGroupID { get; set; }
    public string UserGroupName { get; set; } = "";
    public string UserGroupDisplayText { get; set; } = "";
    public DateTimeOffset RequestTimeStarted { get; set; }
    public DateTimeOffset RequestTimeEnded { get; set; }
    public string RequestTimeElapsed { get; set; } = "";
    public bool Succeeded { get; set; }
    public int CriticalErrorCount { get; set; }
    public int ValidationFailedCount { get; set; }
    public int AppErrorCount { get; set; }
    public int TotalErrorCount { get; set; }
    public int InformationMessageCount { get; set; }
    public string VersionName { get; set; } = "";
    public string VersionKey { get; set; } = "";
    public string VersionRelease { get; set; } = "";
    public string VersionStatus { get; set; } = "";
    public string VersionType { get; set; } = "";
    public int InstallationID { get; set; }
    public string InstallLocation { get; set; } = "";
    public bool IsCurrentInstallation { get; set; }
}
