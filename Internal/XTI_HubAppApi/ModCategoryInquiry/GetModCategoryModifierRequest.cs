namespace XTI_HubAppApi.ModCategoryInquiry;

public sealed class GetModCategoryModifierRequest
{
    public int CategoryID { get; set; }
    public string ModifierKey { get; set; } = "";
}
