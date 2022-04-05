namespace XTI_HubDB.Entities;

public sealed class AppXtiVersionEntity
{
    public int ID { get; set; }
    public int AppID { get; set; }
    public int VersionID { get; set; }
    public string Domain { get; set; } = "";
}
