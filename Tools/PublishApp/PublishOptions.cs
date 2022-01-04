namespace PublishApp;

internal sealed class PublishOptions
{
    public string AppName { get; set; } = "";
    public string AppType { get; set; } = "";
    public string Domain { get; set; } = "";
    public string SiteName { get; set; } = "";
    public string RepoOwner { get; set; } = "";
    public string RepoName { get; set; } = "";
    public bool NoInstall { get; set; }
    public string DestinationMachine { get; set; } = "";
}