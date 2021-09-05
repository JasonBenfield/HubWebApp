using XTI_App;

namespace XTI_HubAppApi.Users
{
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
}
