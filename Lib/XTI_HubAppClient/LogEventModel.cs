// Generated Code
namespace XTI_HubAppClient;
public sealed partial class LogEventModel
{
    public string EventKey { get; set; } = "";
    public string RequestKey { get; set; } = "";
    public int Severity { get; set; }

    public DateTimeOffset TimeOccurred { get; set; }

    public string Caption { get; set; } = "";
    public string Message { get; set; } = "";
    public string Detail { get; set; } = "";
    public int ActualCount { get; set; }
}