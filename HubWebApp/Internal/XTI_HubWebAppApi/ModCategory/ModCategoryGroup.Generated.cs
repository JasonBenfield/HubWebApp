using XTI_HubWebAppApiActions.ModCategoryInquiry;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.ModCategory;
public sealed partial class ModCategoryGroup : AppApiGroupWrapper
{
    internal ModCategoryGroup(AppApiGroup source, ModCategoryGroupBuilder builder) : base(source)
    {
        GetModCategory = builder.GetModCategory.Build();
        GetModifiers = builder.GetModifiers.Build();
        GetResourceGroups = builder.GetResourceGroups.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<int, ModifierCategoryModel> GetModCategory { get; }
    public AppApiAction<int, ModifierModel[]> GetModifiers { get; }
    public AppApiAction<int, ResourceGroupModel[]> GetResourceGroups { get; }
}