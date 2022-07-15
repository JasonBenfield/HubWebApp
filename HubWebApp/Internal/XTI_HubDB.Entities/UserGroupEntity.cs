namespace XTI_HubDB.Entities;

public sealed class UserGroupEntity
{
    public int ID { get; set; }
    public string GroupName { get; set; } = "";
    public string DisplayText { get; set; } = "";
    public string PublicKey { get; set; } = "";
}
