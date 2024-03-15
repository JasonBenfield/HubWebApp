using XTI_App.Api;
using XTI_Core;

namespace XTI_AdminTool;

public sealed class AdminToolOptions
{
    public HubClientOptions HubClient { get; set; } = new();
    public DbOptions DB { get; set; } = new();
}
