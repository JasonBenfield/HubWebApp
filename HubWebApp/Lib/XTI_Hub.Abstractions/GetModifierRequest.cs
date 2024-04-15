using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetModifierRequest
{
    public GetModifierRequest()
        :this(0, ModifierKey.Default)
    {    
    }

    public GetModifierRequest(int categoryID, ModifierKey modKey)
    {
        CategoryID = categoryID;
        ModKey = modKey.DisplayText;
    }

    public int CategoryID { get; set; }
    public string ModKey { get; set; }
}
