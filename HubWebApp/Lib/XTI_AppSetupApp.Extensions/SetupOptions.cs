namespace XTI_AppSetupApp.Extensions;

public sealed class SetupOptions
{
    public static readonly string Setup = nameof(Setup);

    public string VersionKey { get; set; } = "";
    public string VersionsPath { get; set; } = "";
    public string Domain { get; set; } = "";
}