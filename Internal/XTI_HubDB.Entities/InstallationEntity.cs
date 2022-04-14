namespace XTI_HubDB.Entities;

public sealed class InstallationEntity
{
    public int ID { get; set; }
    public int LocationID { get; set; }
    public int AppVersionID { get; set; }
    public int Status { get; set; }
    public bool IsCurrent { get; set; }
    public DateTimeOffset TimeAdded { get; set; } = DateTimeOffset.MaxValue;
}