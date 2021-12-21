namespace BuildWebApp;

internal sealed class BuildOptions
{
    public string AppName { get; set; } = "";

    public string AppsToImport { get; set; } = "";

    public string VersionKey { get; set; } = "";
}