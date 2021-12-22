using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.ResourceInquiry;

public sealed class ResourceInquiryGroup : AppApiGroupWrapper
{
    public ResourceInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new AppApiActionFactory(source);
        GetResource = source.AddAction(actions.Action(nameof(GetResource), () => sp.GetRequiredService<GetResourceAction>()));
        GetRoleAccess = source.AddAction(actions.Action(nameof(GetRoleAccess), () => sp.GetRequiredService<GetRoleAccessAction>()));
        GetMostRecentRequests = source.AddAction(actions.Action(nameof(GetMostRecentRequests), () => sp.GetRequiredService<GetMostRecentRequestsAction>()));
        GetMostRecentErrorEvents = source.AddAction(actions.Action(nameof(GetMostRecentErrorEvents), () => sp.GetRequiredService<GetMostRecentErrorEventsAction>()));
    }

    public AppApiAction<GetResourceRequest, ResourceModel> GetResource { get; }
    public AppApiAction<GetResourceRoleAccessRequest, AppRoleModel[]> GetRoleAccess { get; }
    public AppApiAction<GetResourceLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
    public AppApiAction<GetResourceLogRequest, AppEventModel[]> GetMostRecentErrorEvents { get; }
}