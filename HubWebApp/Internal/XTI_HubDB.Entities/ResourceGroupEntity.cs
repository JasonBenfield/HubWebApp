namespace XTI_HubDB.Entities;

public sealed class ResourceGroupEntity
{
    public int ID { get; set; }
    public int AppVersionID { get; set; }
    public string Name { get; set; } = "";
    public int ModCategoryID { get; set; }
    public bool IsAnonymousAllowed { get; set; }
}