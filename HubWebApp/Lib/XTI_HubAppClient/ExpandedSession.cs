// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ExpandedSession
{
    public int SessionID { get; set; }
    public int UserID { get; set; }
    public string UserName { get; set; } = "";
    public int UserGroupID { get; set; }
    public string UserGroupName { get; set; } = "";
    public string UserGroupDisplayText { get; set; } = "";
    public string RemoteAddress { get; set; } = "";
    public string UserAgent { get; set; } = "";
    public DateTimeOffset TimeSessionStarted { get; set; }
    public DateTimeOffset TimeSessionEnded { get; set; }
    public string TimeElapsed { get; set; } = "";
    public DateTimeOffset? LastRequestTime { get; set; }
    public int RequestCount { get; set; }
}