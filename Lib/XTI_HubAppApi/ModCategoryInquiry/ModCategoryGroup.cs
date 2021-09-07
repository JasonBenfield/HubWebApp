using XTI_App;
using XTI_App.Api;

namespace XTI_HubAppApi.ModCategoryInquiry
{
    public sealed class ModCategoryGroup : AppApiGroupWrapper
    {
        public ModCategoryGroup(AppApiGroup source, ModCategoryGroupActionFactory factory)
            : base(source)
        {
            var actions = new AppApiActionFactory(source);
            GetModCategory = source.AddAction(actions.Action(nameof(GetModCategory), factory.CreateGetModCategory));
            GetModifiers = source.AddAction(actions.Action(nameof(GetModifiers), factory.CreateGetModifiers));
            GetModifier = source.AddAction(actions.Action(nameof(GetModifier), factory.CreateGetModifier));
            GetResourceGroups = source.AddAction(actions.Action(nameof(GetResourceGroups), factory.CreateGetResourceGroups));
        }
        public AppApiAction<int, ModifierCategoryModel> GetModCategory { get; }
        public AppApiAction<int, ModifierModel[]> GetModifiers { get; }
        public AppApiAction<GetModCategoryModifierRequest, ModifierModel> GetModifier { get; }
        public AppApiAction<int, ResourceGroupModel[]> GetResourceGroups { get; }
    }
}
