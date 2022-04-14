namespace XTI_HubDB.Entities;

public sealed class ResourceEntity
{
    public int ID { get; set; }
    public int GroupID { get; set; }
    public string Name { get; set; } = "xti_notfound";
    public bool IsAnonymousAllowed { get; set; }
    public int ResultType { get; set; }
}