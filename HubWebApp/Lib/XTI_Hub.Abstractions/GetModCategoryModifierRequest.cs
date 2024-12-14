using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetModCategoryModifierRequest
{
    public GetModCategoryModifierRequest()
        : this(0, XTI_App.Abstractions.ModifierKey.Default)
    {
    }

    public GetModCategoryModifierRequest(int categoryID, ModifierKey modifierKey)
    {
        CategoryID = categoryID;
        ModifierKey = modifierKey.DisplayText;
    }

    public int CategoryID { get; set; }
    public string ModifierKey { get; set; }
}
