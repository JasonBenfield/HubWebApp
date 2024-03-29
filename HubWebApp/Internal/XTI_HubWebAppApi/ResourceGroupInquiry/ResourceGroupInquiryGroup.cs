﻿namespace XTI_HubWebAppApi.ResourceGroupInquiry;

public sealed class ResourceGroupInquiryGroup : AppApiGroupWrapper
{
    public ResourceGroupInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetResourceGroup = source.AddAction(nameof(GetResourceGroup), () => sp.GetRequiredService<GetResourceGroupAction>());
        GetResources = source.AddAction(nameof(GetResources), () => sp.GetRequiredService<GetResourcesAction>());
        GetRoleAccess = source.AddAction(nameof(GetRoleAccess), () => sp.GetRequiredService<GetRoleAccessAction>());
        GetModCategory = source.AddAction(nameof(GetModCategory), () => sp.GetRequiredService<GetModCategoryAction>());
        GetMostRecentRequests = source.AddAction(nameof(GetMostRecentRequests), () => sp.GetRequiredService<GetMostRecentRequestsAction>());
        GetMostRecentErrorEvents = source.AddAction(nameof(GetMostRecentErrorEvents), () => sp.GetRequiredService<GetMostRecentErrorEventsAction>());
    }
    public AppApiAction<GetResourceGroupRequest, ResourceGroupModel> GetResourceGroup { get; }
    public AppApiAction<GetResourcesRequest, ResourceModel[]> GetResources { get; }
    public AppApiAction<GetResourceGroupRoleAccessRequest, AppRoleModel[]> GetRoleAccess { get; }
    public AppApiAction<GetResourceGroupModCategoryRequest, ModifierCategoryModel> GetModCategory { get; }
    public AppApiAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
    public AppApiAction<GetResourceGroupLogRequest, AppLogEntryModel[]> GetMostRecentErrorEvents { get; }
}