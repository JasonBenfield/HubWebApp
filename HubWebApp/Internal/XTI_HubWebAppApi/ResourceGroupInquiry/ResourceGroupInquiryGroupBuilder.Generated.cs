using XTI_HubWebAppApiActions.ResourceGroupInquiry;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.ResourceGroupInquiry;
public sealed partial class ResourceGroupInquiryGroupBuilder
{
    private readonly AppApiGroup source;
    internal ResourceGroupInquiryGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        GetModCategory = source.AddAction<GetResourceGroupModCategoryRequest, ModifierCategoryModel>("GetModCategory").WithExecution<GetModCategoryAction>();
        GetMostRecentErrorEvents = source.AddAction<GetResourceGroupLogRequest, AppLogEntryModel[]>("GetMostRecentErrorEvents").WithExecution<GetMostRecentErrorEventsAction>();
        GetMostRecentRequests = source.AddAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]>("GetMostRecentRequests").WithExecution<GetMostRecentRequestsAction>();
        GetResourceGroup = source.AddAction<GetResourceGroupRequest, ResourceGroupModel>("GetResourceGroup").WithExecution<GetResourceGroupAction>();
        GetResources = source.AddAction<GetResourcesRequest, ResourceModel[]>("GetResources").WithExecution<GetResourcesAction>();
        GetRoleAccess = source.AddAction<GetResourceGroupRoleAccessRequest, AppRoleModel[]>("GetRoleAccess").WithExecution<GetRoleAccessAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<GetResourceGroupModCategoryRequest, ModifierCategoryModel> GetModCategory { get; }
    public AppApiActionBuilder<GetResourceGroupLogRequest, AppLogEntryModel[]> GetMostRecentErrorEvents { get; }
    public AppApiActionBuilder<GetResourceGroupLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
    public AppApiActionBuilder<GetResourceGroupRequest, ResourceGroupModel> GetResourceGroup { get; }
    public AppApiActionBuilder<GetResourcesRequest, ResourceModel[]> GetResources { get; }
    public AppApiActionBuilder<GetResourceGroupRoleAccessRequest, AppRoleModel[]> GetRoleAccess { get; }

    public ResourceGroupInquiryGroup Build() => new ResourceGroupInquiryGroup(source, this);
}