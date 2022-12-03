namespace XTI_HubDB.Entities;

public sealed class AppEntity
{
    public int ID { get; set; }
    public int Type { get; set; }
    public string Name { get; set; } = "xti_notfound";
    public string DisplayText { get; set; } = "xti_notfound";
    public string Title { get; set; } = "";
    public string VersionName { get; set; } = "";
    public DateTimeOffset TimeAdded { get; set; } = DateTimeOffset.MaxValue;
}