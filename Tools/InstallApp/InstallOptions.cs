namespace InstallApp;

internal sealed class InstallOptions
{
    public string AppName { get; set; } = "";
    public string AppType { get; set; } = "";
    public string RepoOwner { get; set; } = "";
    public string RepoName { get; set; } = "";
    public string DestinationMachine { get; set; } = "";
}