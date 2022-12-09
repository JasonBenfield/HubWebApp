using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AddOrUpdateModifierByTargetKeyRequest
{
    public AddOrUpdateModifierByTargetKeyRequest()
        :this(ModifierCategoryName.Default, GenerateKeyModel.Guid(), "", "")
    {
    }

    public AddOrUpdateModifierByTargetKeyRequest
    (
        ModifierCategoryName modCategoryName, 
        GenerateKeyModel generateModKey, 
        string targetKey, 
        string targetDisplayText
    )
    {
        ModCategoryName = modCategoryName.DisplayText;
        GenerateModKey = generateModKey;
        TargetKey = targetKey;
        TargetDisplayText = targetDisplayText;
    }

    public string ModCategoryName { get; set; }
    public GenerateKeyModel GenerateModKey { get; set; }
    public string TargetKey { get; set; }
    public string TargetDisplayText { get; set; }
}
