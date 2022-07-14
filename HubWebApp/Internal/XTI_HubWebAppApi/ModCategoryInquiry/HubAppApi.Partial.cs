using XTI_HubWebAppApi.ModCategoryInquiry;

namespace XTI_HubWebAppApi;

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