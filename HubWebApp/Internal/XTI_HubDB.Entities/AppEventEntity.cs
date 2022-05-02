namespace XTI_HubDB.Entities;

public sealed class AppEventEntity
{
    public int ID { get; set; }
    public string EventKey { get; set; } = "";
    public int RequestID { get; set; }
    public int Severity { get; set; }
    public string Caption { get; set; } = "";
    public string Message { get; set; } = "";
    public string Detail { get; set; } = "";
    public DateTimeOffset TimeOccurred { get; set; } = DateTimeOffset.MaxValue;
    public int ActualCount { get; set; } = 1;
}