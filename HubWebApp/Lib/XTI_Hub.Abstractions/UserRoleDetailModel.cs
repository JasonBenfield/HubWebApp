using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record UserRoleDetailModel
(
    int ID,
    AppUserModel User,
    AppModel App,
    AppRoleModel Role,
    ModifierCategoryModel ModCategory,
    ModifierModel Modifier
)
{
    public UserRoleDetailModel()
        : this(0, new AppUserModel(), new AppModel(), new AppRoleModel(), new ModifierCategoryModel(), new ModifierModel())
    {
    }
}
