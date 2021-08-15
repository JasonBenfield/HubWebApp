using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebAppApi.Apps
{
    public sealed class ResourceGroupInquiryGroup : AppApiGroupWrapper
    {
        public ResourceGroupInquiryGroup(AppApiGroup source, ResourceGroupInquiryActionFactory factory)
            : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            GetResourceGroup = source.AddAction(actions.Action(nameof(GetResourceGroup), factory.CreateGetResourceGroup));
            GetResources = source.AddAction(actions.Action(nameof(GetResources), factory.CreateGetResources));
            GetRoleAccess = source.AddAction(actions.Action(nameof(GetRoleAccess), factory.CreateGetResourceGroupRoleAccess));
            GetModCategory = source.AddAction(actions.Action(nameof(GetModCategory), factory.CreateGetResourceGroupModCategory));
            GetMostRecentRequests = source.AddAction(actions.Action(nameof(GetMostRecentRequests), factory.CreateGetMostRecentRequests));
            GetMostRecentErrorEvents = source.AddAction(actions.Action(nameof(GetMostRecentErrorEvents), factory.CreateGetMostRecentErrorEvents));
        }
        public AppApiAction<int, ResourceGroupModel> GetResourceGroup { get; }
        public AppApiAction<int, ResourceModel[]> GetResources { get; }
        public AppApiAction<int, AppRoleModel[]> GetRoleAccess { get; }
        public AppApiAction<int, ModifierCategoryModel> GetModCategory { get; }
        public AppApiAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
        public AppApiAction<GetResourceGroupLogRequest, AppEventModel[]> GetMostRecentErrorEvents { get; }
    }
}
