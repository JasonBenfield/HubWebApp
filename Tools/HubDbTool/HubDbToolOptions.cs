namespace HubDbTool;

internal sealed class HubDbToolOptions
{
    public string Command { get; set; } = "";
    public string BackupFilePath { get; set; } = "";
    public bool Force { get; set; }
}