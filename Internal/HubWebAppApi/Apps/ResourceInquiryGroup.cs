using XTI_App;
using XTI_App.Api;

namespace HubWebAppApi.Apps
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

        public AppApiAction<int, ResourceModel> GetResource { get; }
        public AppApiAction<int, AppRoleModel[]> GetRoleAccess { get; }
        public AppApiAction<GetResourceLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
        public AppApiAction<GetResourceLogRequest, AppEventModel[]> GetMostRecentErrorEvents { get; }
    }
}
