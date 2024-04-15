using XTI_App.Api;
using XTI_Core;

namespace XTI_AppSetupApp.Extensions;

public sealed class SetupOptions
{
    public string VersionName { get; set; } = "";
    public string VersionKey { get; set; } = "";

    public XtiTokenOptions XtiToken { get; set; } = new();
    public HubClientOptions HubClient { get; set; } = new();
    public DbOptions DB { get; set; } = new();
}