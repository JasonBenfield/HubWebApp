using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AddOrUpdateModifierByModKeyRequest
{
    public AddOrUpdateModifierByModKeyRequest()
        :this(ModifierCategoryName.Default, ModifierKey.Default, "", "")
    {
    }

    public AddOrUpdateModifierByModKeyRequest
    (
        ModifierCategoryName modCategoryName, 
        ModifierKey modKey, 
        string targetKey, 
        string targetDisplayText
    )
    {
        ModCategoryName = modCategoryName.DisplayText;
        ModKey = modKey.DisplayText;
        TargetKey = targetKey;
        TargetDisplayText = targetDisplayText;
    }

    public string ModCategoryName { get; set; }
    public string ModKey { get; set; }
    public string TargetKey { get; set; }
    public string TargetDisplayText { get; set; }
}
