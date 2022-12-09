using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class SetUserAccessRoleRequest
{
    public SetUserAccessRoleRequest()
        : this(AppKey.Unknown, ModifierCategoryName.Default, ModifierKey.Default, new AppRoleName[0])
    {
    }

    public SetUserAccessRoleRequest(AppKey appKey, params AppRoleName[] roleNames)
        : this(appKey, ModifierCategoryName.Default, ModifierKey.Default, roleNames)
    {
    }

    public SetUserAccessRoleRequest(AppKey appKey, ModifierCategoryName modCategoryName, ModifierKey modKey, params AppRoleName[] roleNames)
    {
        AppKey = appKey;
        ModCategoryName = modCategoryName.DisplayText;
        ModKey = modKey.DisplayText;
        RoleNames = roleNames.Select(rn => rn.DisplayText).ToArray();
    }

    public AppKey AppKey { get; set; }
    public string ModCategoryName { get; set; }
    public string ModKey { get; set; }
    public string[] RoleNames { get; set; }
}
