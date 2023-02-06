using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class SystemAddOrUpdateModifierByTargetKeyRequest
{
    public SystemAddOrUpdateModifierByTargetKeyRequest()
        :this(0, ModifierCategoryName.Default, GenerateKeyModel.Guid(), "", "")
    {
    }

    public SystemAddOrUpdateModifierByTargetKeyRequest
    (
        int installationID,
        ModifierCategoryName modCategoryName, 
        GenerateKeyModel generateModKey, 
        string targetKey, 
        string targetDisplayText
    )
    {
        InstallationID = installationID;
        ModCategoryName = modCategoryName.DisplayText;
        GenerateModKey = generateModKey;
        TargetKey = targetKey;
        TargetDisplayText = targetDisplayText;
    }

    public int InstallationID { get; set; }
    public string ModCategoryName { get; set; }
    public GenerateKeyModel GenerateModKey { get; set; }
    public string TargetKey { get; set; }
    public string TargetDisplayText { get; set; }
}
