namespace HubDbTool
{
    public sealed class MainDbToolOptions
    {
        public string Command { get; set; }
        public string BackupFilePath { get; set; }
        public bool Force { get; set; }
    }
}
