using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class SystemGetUsersWithAnyRoleRequest
{
    public SystemGetUsersWithAnyRoleRequest()
        : this(ModifierCategoryName.Default, ModifierKey.Default, [])
    {
    }

    public SystemGetUsersWithAnyRoleRequest(params AppRoleName[] roleNames)
        : this(ModifierCategoryName.Default, ModifierKey.Default, roleNames)
    {
    }

    public SystemGetUsersWithAnyRoleRequest(ModifierCategoryName modCategoryName, ModifierKey modKey, params AppRoleName[] roleNames)
    {
        ModCategoryName = modCategoryName.DisplayText;
        ModKey = modKey.DisplayText;
        RoleNames = roleNames.Select(rn => rn.DisplayText).ToArray();
    }

    public string ModCategoryName { get; set; }
    public string ModKey { get; set; }
    public string[] RoleNames { get; set; }
}
