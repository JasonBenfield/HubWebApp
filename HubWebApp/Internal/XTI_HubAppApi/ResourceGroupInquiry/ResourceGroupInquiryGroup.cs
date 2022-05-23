namespace XTI_HubAppApi.ResourceGroupInquiry;

public sealed class ResourceGroupInquiryGroup : AppApiGroupWrapper
{
    public ResourceGroupInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        GetResourceGroup = source.AddAction(actions.Action(nameof(GetResourceGroup), () => sp.GetRequiredService<GetResourceGroupAction>()));
        GetResources = source.AddAction(actions.Action(nameof(GetResources), () => sp.GetRequiredService<GetResourcesAction>()));
        GetResource = source.AddAction(actions.Action(nameof(GetResource), () => sp.GetRequiredService<GetResourceAction>()));
        GetRoleAccess = source.AddAction(actions.Action(nameof(GetRoleAccess), () => sp.GetRequiredService<GetRoleAccessAction>()));
        GetModCategory = source.AddAction(actions.Action(nameof(GetModCategory), () => sp.GetRequiredService<GetModCategoryAction>()));
        GetMostRecentRequests = source.AddAction(actions.Action(nameof(GetMostRecentRequests), () => sp.GetRequiredService<GetMostRecentRequestsAction>()));
        GetMostRecentErrorEvents = source.AddAction(actions.Action(nameof(GetMostRecentErrorEvents), () => sp.GetRequiredService<GetMostRecentErrorEventsAction>()));
    }
    public AppApiAction<GetResourceGroupRequest, ResourceGroupModel> GetResourceGroup { get; }
    public AppApiAction<GetResourcesRequest, ResourceModel[]> GetResources { get; }
    public AppApiAction<GetResourceGroupResourceRequest, ResourceModel> GetResource { get; }
    public AppApiAction<GetResourceGroupRoleAccessRequest, AppRoleModel[]> GetRoleAccess { get; }
    public AppApiAction<GetResourceGroupModCategoryRequest, ModifierCategoryModel> GetModCategory { get; }
    public AppApiAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
    public AppApiAction<GetResourceGroupLogRequest, AppLogEntryModel[]> GetMostRecentErrorEvents { get; }
}