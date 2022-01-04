namespace LocalInstallApp;

public sealed class InstallOptions
{
    public string AppName { get; set; } = "";
    public string AppType { get; set; } = "";
    public string VersionKey { get; set; } = "";
    public string InstallationUserName { get; set; } = "";
    public string InstallationPassword { get; set; } = "";
    public string Domain { get; set; } = "";
    public string SiteName { get; set; } = "";
    public string RepoOwner { get; set; } = "";
    public string RepoName { get; set; } = "";
    public string Release { get; set; } = "";
    public string MachineName { get; set; } = "";
}