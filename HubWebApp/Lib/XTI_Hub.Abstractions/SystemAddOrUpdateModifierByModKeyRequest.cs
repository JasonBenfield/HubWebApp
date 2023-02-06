using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class SystemAddOrUpdateModifierByModKeyRequest
{
    public SystemAddOrUpdateModifierByModKeyRequest()
        :this(0, ModifierCategoryName.Default, ModifierKey.Default, "", "")
    {
    }

    public SystemAddOrUpdateModifierByModKeyRequest
    (
        int installationID,
        ModifierCategoryName modCategoryName, 
        ModifierKey modKey, 
        string targetKey, 
        string targetDisplayText
    )
    {
        InstallationID = installationID;
        ModCategoryName = modCategoryName.DisplayText;
        ModKey = modKey.DisplayText;
        TargetKey = targetKey;
        TargetDisplayText = targetDisplayText;
    }

    public int InstallationID { get; set; }
    public string ModCategoryName { get; set; }
    public string ModKey { get; set; }
    public string TargetKey { get; set; }
    public string TargetDisplayText { get; set; }
}
