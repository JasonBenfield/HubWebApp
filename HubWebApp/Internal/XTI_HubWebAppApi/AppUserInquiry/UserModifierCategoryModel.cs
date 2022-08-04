namespace XTI_HubWebAppApi.AppUserInquiry;

public sealed class UserModifierCategoryModel
{
    public ModifierCategoryModel ModCategory { get; set; } = new ModifierCategoryModel();
    public ModifierModel[] Modifiers { get; set; } = new ModifierModel[0];
}