using XTI_App;

namespace HubWebAppApi.Users
{
    public sealed class UserModifierCategoryModel
    {
        public ModifierCategoryModel ModCategory { get; set; }
        public ModifierModel[] Modifiers { get; set; }
    }
}
