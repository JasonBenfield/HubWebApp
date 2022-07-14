namespace XTI_HubWebAppApi.ModCategoryInquiry;

public sealed class ModCategoryGroup : AppApiGroupWrapper
{
    public ModCategoryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetModCategory = source.AddAction(nameof(GetModCategory), () => sp.GetRequiredService<GetModCategoryAction>());
        GetModifiers = source.AddAction(nameof(GetModifiers), () => sp.GetRequiredService<GetModifiersAction>());
        GetModifier = source.AddAction(nameof(GetModifier), () => sp.GetRequiredService<GetModifierAction>());
        GetResourceGroups = source.AddAction(nameof(GetResourceGroups), () => sp.GetRequiredService<GetResourceGroupsAction>());
    }
    public AppApiAction<int, ModifierCategoryModel> GetModCategory { get; }
    public AppApiAction<int, ModifierModel[]> GetModifiers { get; }
    public AppApiAction<GetModCategoryModifierRequest, ModifierModel> GetModifier { get; }
    public AppApiAction<int, ResourceGroupModel[]> GetResourceGroups { get; }
}