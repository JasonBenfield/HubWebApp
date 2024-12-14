using XTI_HubWebAppApiActions.ModCategoryInquiry;

// Generated Code
namespace XTI_HubWebAppApi.ModCategory;
public sealed partial class ModCategoryGroupBuilder
{
    private readonly AppApiGroup source;
    internal ModCategoryGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        GetModCategory = source.AddAction<int, ModifierCategoryModel>("GetModCategory").WithExecution<GetModCategoryAction>();
        GetModifiers = source.AddAction<int, ModifierModel[]>("GetModifiers").WithExecution<GetModifiersAction>();
        GetResourceGroups = source.AddAction<int, ResourceGroupModel[]>("GetResourceGroups").WithExecution<GetResourceGroupsAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<int, ModifierCategoryModel> GetModCategory { get; }
    public AppApiActionBuilder<int, ModifierModel[]> GetModifiers { get; }
    public AppApiActionBuilder<int, ResourceGroupModel[]> GetResourceGroups { get; }

    public ModCategoryGroup Build() => new ModCategoryGroup(source, this);
}