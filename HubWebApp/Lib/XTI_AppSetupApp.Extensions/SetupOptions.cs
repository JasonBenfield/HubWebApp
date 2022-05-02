namespace XTI_AppSetupApp.Extensions;

public sealed class SetupOptions
{
    public static readonly string Setup = nameof(Setup);

    public string VersionName { get; set; } = "";
    public string VersionKey { get; set; } = "";
}