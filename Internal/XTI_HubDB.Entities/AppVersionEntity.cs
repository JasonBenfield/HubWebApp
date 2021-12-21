namespace XTI_HubDB.Entities;

public sealed class AppVersionEntity
{
    public int ID { get; set; }
    public string VersionKey { get; set; } = "xti_notfound";
    public int AppID { get; set; }
    public int Major { get; set; }
    public int Minor { get; set; }
    public int Patch { get; set; }
    public int Status { get; set; }
    public int Type { get; set; }
    public string Description { get; set; } = "";
    public DateTimeOffset TimeAdded { get; set; } = DateTimeOffset.MaxValue;
}