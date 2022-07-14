namespace XTI_HubWebAppApi.ModCategoryInquiry;

public sealed class GetModCategoryModifierRequest
{
    public int CategoryID { get; set; }
    public string ModifierKey { get; set; } = "";
}
