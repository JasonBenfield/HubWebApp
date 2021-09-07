using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.ResourceGroupInquiry
{
    public sealed class ResourceGroupInquiryGroup : AppApiGroupWrapper
    {
        public ResourceGroupInquiryGroup(AppApiGroup source, ResourceGroupInquiryActionFactory factory)
            : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            GetResourceGroup = source.AddAction(actions.Action(nameof(GetResourceGroup), factory.CreateGetResourceGroup));
            GetResources = source.AddAction(actions.Action(nameof(GetResources), factory.CreateGetResources));
            GetResource = source.AddAction(actions.Action(nameof(GetResource), factory.CreateGetResource));
            GetRoleAccess = source.AddAction(actions.Action(nameof(GetRoleAccess), factory.CreateGetResourceGroupRoleAccess));
            GetModCategory = source.AddAction(actions.Action(nameof(GetModCategory), factory.CreateGetResourceGroupModCategory));
            GetMostRecentRequests = source.AddAction(actions.Action(nameof(GetMostRecentRequests), factory.CreateGetMostRecentRequests));
            GetMostRecentErrorEvents = source.AddAction(actions.Action(nameof(GetMostRecentErrorEvents), factory.CreateGetMostRecentErrorEvents));
        }
        public AppApiAction<GetResourceGroupRequest, ResourceGroupModel> GetResourceGroup { get; }
        public AppApiAction<GetResourcesRequest, ResourceModel[]> GetResources { get; }
        public AppApiAction<GetResourceGroupResourceRequest, ResourceModel> GetResource { get; }
        public AppApiAction<GetResourceGroupRoleAccessRequest, AppRoleModel[]> GetRoleAccess { get; }
        public AppApiAction<GetResourceGroupModCategoryRequest, ModifierCategoryModel> GetModCategory { get; }
        public AppApiAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
        public AppApiAction<GetResourceGroupLogRequest, AppEventModel[]> GetMostRecentErrorEvents { get; }
    }
}
