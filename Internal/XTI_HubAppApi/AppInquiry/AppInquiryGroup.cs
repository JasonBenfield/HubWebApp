using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App.Api;
using XTI_Hub;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.AppInquiry
{
    public sealed class AppInquiryGroup : AppApiGroupWrapper
    {
        public AppInquiryGroup(AppApiGroup source, IServiceProvider sp)
            : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            Index = source.AddAction(actions.View(nameof(Index), () => sp.GetRequiredService<IndexAction>()));
            GetApp = source.AddAction(actions.Action(nameof(GetApp), () => sp.GetRequiredService<GetAppAction>()));
            GetRoles = source.AddAction(actions.Action(nameof(GetRoles), () => sp.GetRequiredService<GetRolesAction>()));
            GetRole = source.AddAction(actions.Action(nameof(GetRole), () => sp.GetRequiredService<GetRoleAction>()));
            GetResourceGroups = source.AddAction(actions.Action(nameof(GetResourceGroups), () => sp.GetRequiredService<GetResourceGroupsAction>()));
            GetMostRecentRequests = source.AddAction(actions.Action(nameof(GetMostRecentRequests), () => sp.GetRequiredService<GetMostRecentRequestsAction>()));
            GetMostRecentErrorEvents = source.AddAction(actions.Action(nameof(GetMostRecentErrorEvents), () => sp.GetRequiredService<GetMostRecentErrorEventsAction>()));
            GetModifierCategories = source.AddAction(actions.Action(nameof(GetModifierCategories), () => sp.GetRequiredService<GetModifierCategoriesAction>()));
            GetModifierCategory = source.AddAction(actions.Action(nameof(GetModifierCategory), () => sp.GetRequiredService<GetModifierCategoryAction>()));
            GetDefaultModiifer = source.AddAction(actions.Action(nameof(GetDefaultModiifer), () => sp.GetRequiredService<GetDefaultModifierAction>()));
        }

        public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
        public AppApiAction<EmptyRequest, AppModel> GetApp { get; }
        public AppApiAction<EmptyRequest, AppRoleModel[]> GetRoles { get; }
        public AppApiAction<string, AppRoleModel> GetRole { get; }
        public AppApiAction<EmptyRequest, ResourceGroupModel[]> GetResourceGroups { get; }
        public AppApiAction<int, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
        public AppApiAction<int, AppEventModel[]> GetMostRecentErrorEvents { get; }
        public AppApiAction<EmptyRequest, ModifierCategoryModel[]> GetModifierCategories { get; }
        public AppApiAction<string, ModifierCategoryModel> GetModifierCategory { get; }
        public AppApiAction<EmptyRequest, ModifierModel> GetDefaultModiifer { get; }
    }
}
