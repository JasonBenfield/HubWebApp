using XTI_App;

namespace XTI_HubAppApi.AppUserInquiry
{
    public sealed class UserModifierCategoryModel
    {
        public ModifierCategoryModel ModCategory { get; set; }
        public ModifierModel[] Modifiers { get; set; }
    }
}
