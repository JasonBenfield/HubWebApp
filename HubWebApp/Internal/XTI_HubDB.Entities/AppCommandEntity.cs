namespace XTI_HubDB.Entities;

public sealed class AppCommandEntity
{
    public int ID { get; set; }
    public int AppID { get; set; }
    public string CommandName { get; set; } = "";
    public string SerializedRequest { get; set; } = "";
    public DateTimeOffset TimeAdded { get; set; } = DateTimeOffset.MaxValue;
    public DateTimeOffset TimeStarted { get; set; } = DateTimeOffset.MaxValue;
    public DateTimeOffset TimeEnded { get; set; } = DateTimeOffset.MaxValue;
}
