using XTI_App.Abstractions;

namespace XTI_Admin;

public sealed class AdminOptions
{
    public CommandNames Command { get; set; }
    public string AppName { get; set; } = "";
    public string AppType { get; set; } = "";
    public string VersionKey { get; set; } = "";
    public string RepoOwner { get; set; } = "";
    public string RepoName { get; set; } = "";
    public string Domain { get; set; } = "";
    public string SiteName { get; set; } = "";
    public string DestinationMachine { get; set; } = "";
    public string InstallationUserName { get; set; } = "";
    public string InstallationPassword { get; set; } = "";
    public InstallationSources InstallationSource { get; set; } = InstallationSources.Default;
    public string Release { get; set; } = "";
    public string VersionType { get; set; } = "";
    public string IssueTitle { get; set; } = "";
    public int IssueNumber { get; set; }
    public bool StartIssue { get; set; }
    public HubAdministrationTypes HubAdministrationType { get; set; } = HubAdministrationTypes.Default;

    public AppKey AppKey() =>
        string.IsNullOrWhiteSpace(AppName) 
        ? XTI_App.Abstractions.AppKey.Unknown
        : new AppKey(new AppName(AppName), XTI_App.Abstractions.AppType.Values.Value(AppType));
}