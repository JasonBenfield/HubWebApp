namespace PublishApp;

internal sealed class PublishOptions
{
    public string AppName { get; set; } = "";
    public string AppType { get; set; } = "";
    public string RepoOwner { get; set; } = "";
    public string RepoName { get; set; } = "";
    public string AppsToImport { get; set; } = "";
    public bool NoInstall { get; set; }
    public string DestinationMachine { get; set; } = "";
}