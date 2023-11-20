namespace XTI_HubWebAppApi.Logs;

public sealed class AppRequestQueryRequest
{
    public int? SessionID { get; set; }
    public int? InstallationID { get; set; }
    public int? SourceRequestID { get; set; }
}
