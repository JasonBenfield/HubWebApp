using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class SystemGetUsersWithAnyRoleRequest
{
    public SystemGetUsersWithAnyRoleRequest()
        : this(0, ModifierCategoryName.Default, ModifierKey.Default, new AppRoleName[0])
    {
    }

    public SystemGetUsersWithAnyRoleRequest(int installationID, params AppRoleName[] roleNames)
        : this(installationID, ModifierCategoryName.Default, ModifierKey.Default, roleNames)
    {
    }

    public SystemGetUsersWithAnyRoleRequest(int installationID, ModifierCategoryName modCategoryName, ModifierKey modKey, params AppRoleName[] roleNames)
    {
        InstallationID = installationID;
        ModCategoryName = modCategoryName.DisplayText;
        ModKey = modKey.DisplayText;
        RoleNames = roleNames.Select(rn => rn.DisplayText).ToArray();
    }

    public int InstallationID { get; set; }
    public string ModCategoryName { get; set; }
    public string ModKey { get; set; }
    public string[] RoleNames { get; set; }
}
