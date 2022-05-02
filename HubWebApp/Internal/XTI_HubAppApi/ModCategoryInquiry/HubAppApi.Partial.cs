using XTI_HubAppApi.ModCategoryInquiry;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private ModCategoryGroup? modCategory;

    public ModCategoryGroup ModCategory { get => modCategory ?? throw new ArgumentNullException(nameof(modCategory)); }

    partial void createModCategory(IServiceProvider sp)
    {
        modCategory = new ModCategoryGroup
        (
            source.AddGroup
            (
                nameof(ModCategory),
                HubInfo.ModCategories.Apps,
                Access.WithAllowed(HubInfo.Roles.ViewApp)
            ),
            sp
        );
    }
}