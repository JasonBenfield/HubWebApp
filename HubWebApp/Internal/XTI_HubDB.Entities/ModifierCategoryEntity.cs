namespace XTI_HubDB.Entities;

public sealed class ModifierCategoryEntity
{
    public int ID { get; set; }
    public int AppID { get; set; }
    public string Name { get; set; } = "xti_notfound";
    public string DisplayText { get; set; } = "";
}