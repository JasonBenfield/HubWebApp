using XTI_Hub;
using XTI_App.Api;

namespace XTI_HubAppApi.ResourceInquiry
{
    public sealed class ResourceInquiryGroup : AppApiGroupWrapper
    {
        public ResourceInquiryGroup(AppApiGroup source, ResourceInquiryActionFactory factory) : base(source)
        {
            var actions = new AppApiActionFactory(source);
            GetResource = source.AddAction(actions.Action(nameof(GetResource), factory.CreateGetResource));
            GetRoleAccess = source.AddAction(actions.Action(nameof(GetRoleAccess), factory.CreateGetResourceRoleAccess));
            GetMostRecentRequests = source.AddAction(actions.Action(nameof(GetMostRecentRequests), factory.CreateGetMostRecentRequests));
            GetMostRecentErrorEvents = source.AddAction(actions.Action(nameof(GetMostRecentErrorEvents), factory.CreateGetMostRecentErrorEvents));
        }

        public AppApiAction<GetResourceRequest, ResourceModel> GetResource { get; }
        public AppApiAction<GetResourceRoleAccessRequest, AppRoleModel[]> GetRoleAccess { get; }
        public AppApiAction<GetResourceLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
        public AppApiAction<GetResourceLogRequest, AppEventModel[]> GetMostRecentErrorEvents { get; }
    }
}
