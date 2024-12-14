using XTI_HubWebAppApiActions.ResourceGroupInquiry;

// Generated Code
namespace XTI_HubWebAppApi.ResourceGroupInquiry;
public sealed partial class ResourceGroupInquiryGroup : AppApiGroupWrapper
{
    internal ResourceGroupInquiryGroup(AppApiGroup source, ResourceGroupInquiryGroupBuilder builder) : base(source)
    {
        GetModCategory = builder.GetModCategory.Build();
        GetMostRecentErrorEvents = builder.GetMostRecentErrorEvents.Build();
        GetMostRecentRequests = builder.GetMostRecentRequests.Build();
        GetResourceGroup = builder.GetResourceGroup.Build();
        GetResources = builder.GetResources.Build();
        GetRoleAccess = builder.GetRoleAccess.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<GetResourceGroupModCategoryRequest, ModifierCategoryModel> GetModCategory { get; }
    public AppApiAction<GetResourceGroupLogRequest, AppLogEntryModel[]> GetMostRecentErrorEvents { get; }
    public AppApiAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
    public AppApiAction<GetResourceGroupRequest, ResourceGroupModel> GetResourceGroup { get; }
    public AppApiAction<GetResourcesRequest, ResourceModel[]> GetResources { get; }
    public AppApiAction<GetResourceGroupRoleAccessRequest, AppRoleModel[]> GetRoleAccess { get; }
}