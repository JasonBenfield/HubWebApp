using XTI_HubWebAppApiActions.ResourceInquiry;

// Generated Code
namespace XTI_HubWebAppApi.ResourceInquiry;
public sealed partial class ResourceInquiryGroupBuilder
{
    private readonly AppApiGroup source;
    internal ResourceInquiryGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        GetMostRecentErrorEvents = source.AddAction<GetResourceLogRequest, AppLogEntryModel[]>("GetMostRecentErrorEvents").WithExecution<GetMostRecentErrorEventsAction>();
        GetMostRecentRequests = source.AddAction<GetResourceLogRequest, AppRequestExpandedModel[]>("GetMostRecentRequests").WithExecution<GetMostRecentRequestsAction>();
        GetResource = source.AddAction<GetResourceRequest, ResourceModel>("GetResource").WithExecution<GetResourceAction>();
        GetRoleAccess = source.AddAction<GetResourceRoleAccessRequest, AppRoleModel[]>("GetRoleAccess").WithExecution<GetRoleAccessAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<GetResourceLogRequest, AppLogEntryModel[]> GetMostRecentErrorEvents { get; }
    public AppApiActionBuilder<GetResourceLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
    public AppApiActionBuilder<GetResourceRequest, ResourceModel> GetResource { get; }
    public AppApiActionBuilder<GetResourceRoleAccessRequest, AppRoleModel[]> GetRoleAccess { get; }

    public ResourceInquiryGroup Build() => new ResourceInquiryGroup(source, this);
}