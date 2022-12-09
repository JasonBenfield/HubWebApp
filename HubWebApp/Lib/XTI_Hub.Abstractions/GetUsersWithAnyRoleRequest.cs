using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetUsersWithAnyRoleRequest
{
    public GetUsersWithAnyRoleRequest()
        : this(ModifierCategoryName.Default, ModifierKey.Default, new AppRoleName[0])
    {
    }

    public GetUsersWithAnyRoleRequest(params AppRoleName[] roleNames)
        : this(ModifierCategoryName.Default, ModifierKey.Default, roleNames)
    {
    }

    public GetUsersWithAnyRoleRequest(ModifierCategoryName modCategoryName, ModifierKey modKey, params AppRoleName[] roleNames)
    {
        ModCategoryName = modCategoryName.DisplayText;
        ModKey = modKey.DisplayText;
        RoleNames = roleNames.Select(rn => rn.DisplayText).ToArray();
    }

    public string ModCategoryName { get; set; }
    public string ModKey { get; set; }
    public string[] RoleNames { get; set; }
}
