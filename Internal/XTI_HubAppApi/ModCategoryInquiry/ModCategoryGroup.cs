using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.ModCategoryInquiry
{
    public sealed class ModCategoryGroup : AppApiGroupWrapper
    {
        public ModCategoryGroup(AppApiGroup source, IServiceProvider sp)
            : base(source)
        {
            var actions = new AppApiActionFactory(source);
            GetModCategory = source.AddAction(actions.Action(nameof(GetModCategory), () => sp.GetRequiredService<GetModCategoryAction>()));
            GetModifiers = source.AddAction(actions.Action(nameof(GetModifiers), () => sp.GetRequiredService<GetModifiersAction>()));
            GetModifier = source.AddAction(actions.Action(nameof(GetModifier), () => sp.GetRequiredService<GetModifierAction>()));
            GetResourceGroups = source.AddAction(actions.Action(nameof(GetResourceGroups), () => sp.GetRequiredService<GetResourceGroupsAction>()));
        }
        public AppApiAction<int, ModifierCategoryModel> GetModCategory { get; }
        public AppApiAction<int, ModifierModel[]> GetModifiers { get; }
        public AppApiAction<GetModCategoryModifierRequest, ModifierModel> GetModifier { get; }
        public AppApiAction<int, ResourceGroupModel[]> GetResourceGroups { get; }
    }
}
