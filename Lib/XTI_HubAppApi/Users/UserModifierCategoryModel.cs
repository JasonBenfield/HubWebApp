using XTI_App;

namespace XTI_HubAppApi.Users
{
    public sealed class UserModifierCategoryModel
    {
        public ModifierCategoryModel ModCategory { get; set; }
        public ModifierModel[] Modifiers { get; set; }
    }
}
