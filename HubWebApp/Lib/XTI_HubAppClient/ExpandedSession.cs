// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ExpandedSession
{
    public int SessionID { get; set; }

    public string UserName { get; set; } = "";
    public string UserGroupName { get; set; } = "";
    public string RemoteAddress { get; set; } = "";
    public string UserAgent { get; set; } = "";
    public DateTimeOffset TimeStarted { get; set; }

    public DateTimeOffset TimeEnded { get; set; }

    public DateTimeOffset? LastRequestTime { get; set; }

    public int RequestCount { get; set; }
}