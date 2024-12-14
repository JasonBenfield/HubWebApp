using XTI_HubWebAppApiActions.ResourceInquiry;

// Generated Code
namespace XTI_HubWebAppApi.ResourceInquiry;
public sealed partial class ResourceInquiryGroup : AppApiGroupWrapper
{
    internal ResourceInquiryGroup(AppApiGroup source, ResourceInquiryGroupBuilder builder) : base(source)
    {
        GetMostRecentErrorEvents = builder.GetMostRecentErrorEvents.Build();
        GetMostRecentRequests = builder.GetMostRecentRequests.Build();
        GetResource = builder.GetResource.Build();
        GetRoleAccess = builder.GetRoleAccess.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<GetResourceLogRequest, AppLogEntryModel[]> GetMostRecentErrorEvents { get; }
    public AppApiAction<GetResourceLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
    public AppApiAction<GetResourceRequest, ResourceModel> GetResource { get; }
    public AppApiAction<GetResourceRoleAccessRequest, AppRoleModel[]> GetRoleAccess { get; }
}