namespace XTI_HubWebAppApi.ResourceInquiry;

public sealed class ResourceInquiryGroup : AppApiGroupWrapper
{
    public ResourceInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetResource = source.AddAction(nameof(GetResource), () => sp.GetRequiredService<GetResourceAction>());
        GetRoleAccess = source.AddAction(nameof(GetRoleAccess), () => sp.GetRequiredService<GetRoleAccessAction>());
        GetMostRecentRequests = source.AddAction(nameof(GetMostRecentRequests), () => sp.GetRequiredService<GetMostRecentRequestsAction>());
        GetMostRecentErrorEvents = source.AddAction(nameof(GetMostRecentErrorEvents), () => sp.GetRequiredService<GetMostRecentErrorEventsAction>());
    }

    public AppApiAction<GetResourceRequest, ResourceModel> GetResource { get; }
    public AppApiAction<GetResourceRoleAccessRequest, AppRoleModel[]> GetRoleAccess { get; }
    public AppApiAction<GetResourceLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
    public AppApiAction<GetResourceLogRequest, AppLogEntryModel[]> GetMostRecentErrorEvents { get; }
}