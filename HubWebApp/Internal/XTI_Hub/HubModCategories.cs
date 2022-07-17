using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class HubModCategories
{
    internal static readonly HubModCategories Instance = new();

    private HubModCategories()
    {
        Default = ModifierCategoryName.Default;
        Apps = new ModifierCategoryName(nameof(Apps));
        UserGroups = new ModifierCategoryName(nameof(UserGroups));
    }

    public ModifierCategoryName Default { get; }
    public ModifierCategoryName Apps { get; }
    public ModifierCategoryName UserGroups { get; }
}