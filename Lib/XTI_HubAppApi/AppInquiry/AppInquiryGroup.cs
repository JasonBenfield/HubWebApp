using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.AppInquiry
{
    public sealed class AppInquiryGroup : AppApiGroupWrapper
    {
        public AppInquiryGroup(AppApiGroup source, AppInquiryActionFactory factory)
            : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            Index = source.AddAction(actions.View(nameof(Index), factory.CreateIndex));
            GetApp = source.AddAction(actions.Action(nameof(GetApp), factory.CreateGetApp));
            GetRoles = source.AddAction(actions.Action(nameof(GetRoles), factory.CreateGetRoles));
            GetRole = source.AddAction(actions.Action(nameof(GetRole), factory.CreateGetRole));
            GetResourceGroups = source.AddAction(actions.Action(nameof(GetResourceGroups), factory.CreateGetResourceGroups));
            GetMostRecentRequests = source.AddAction(actions.Action(nameof(GetMostRecentRequests), factory.CreateGetMostRecentRequests));
            GetMostRecentErrorEvents = source.AddAction(actions.Action(nameof(GetMostRecentErrorEvents), factory.CreateGetMostRecentErrorEvents));
            GetModifierCategories = source.AddAction(actions.Action(nameof(GetModifierCategories), factory.CreateGetModifierCategories));
            GetModifierCategory = source.AddAction(actions.Action(nameof(GetModifierCategory), factory.CreateGetModifierCategory));
            GetDefaultModiifer = source.AddAction(actions.Action(nameof(GetDefaultModiifer), factory.CreateGetDefaultModifier));
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
