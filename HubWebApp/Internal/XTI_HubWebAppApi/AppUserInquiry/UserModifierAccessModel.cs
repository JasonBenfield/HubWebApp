namespace XTI_HubWebAppApi.AppUserInquiry;

public sealed class UserModifierAccessModel
{
    public UserModifierAccessModel(ModifierModel[] unassignedModifiers, UserModifierCategoryModel[] userModCategory)
    {
        UnassignedModifiers = unassignedModifiers;
        UserModCategory = userModCategory;
    }

    public ModifierModel[] UnassignedModifiers { get; }
    public UserModifierCategoryModel[] UserModCategory { get; }
}