using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.Apps
{
    public sealed class AppInquiryGroup : AppApiGroupWrapper
    {
        public AppInquiryGroup(AppApiGroup source, AppInquiryActionFactory factory)
            : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            Index = source.AddAction(actions.View(nameof(Index), factory.CreateIndex));
            GetApp = source.AddAction(actions.Action(nameof(GetApp), factory.CreateGetApp));
            GetCurrentVersion = source.AddAction(actions.Action(nameof(GetCurrentVersion), factory.CreateGetCurrentVersion));
            GetResourceGroups = source.AddAction(actions.Action(nameof(GetResourceGroups), factory.CreateGetResourceGroups));
            GetMostRecentRequests = source.AddAction(actions.Action(nameof(GetMostRecentRequests), factory.CreateGetMostRecentRequests));
            GetMostRecentErrorEvents = source.AddAction(actions.Action(nameof(GetMostRecentErrorEvents), factory.CreateGetMostRecentErrorEvents));
            GetModifierCategories = source.AddAction(actions.Action(nameof(GetModifierCategories), factory.CreateGetModifierCategories));
        }

        public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
        public AppApiAction<EmptyRequest, AppModel> GetApp { get; }
        public AppApiAction<EmptyRequest, AppVersionModel> GetCurrentVersion { get; }
        public AppApiAction<EmptyRequest, ResourceGroupModel[]> GetResourceGroups { get; }
        public AppApiAction<int, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
        public AppApiAction<int, AppEventModel[]> GetMostRecentErrorEvents { get; }
        public AppApiAction<EmptyRequest, ModifierCategoryModel[]> GetModifierCategories { get; }
    }
}
