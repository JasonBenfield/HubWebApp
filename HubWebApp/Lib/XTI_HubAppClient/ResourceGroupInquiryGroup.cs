// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ResourceGroupInquiryGroup : AppClientGroup
{
    public ResourceGroupInquiryGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "ResourceGroupInquiry")
    {
        Actions = new ResourceGroupInquiryGroupActions(GetModCategory: CreatePostAction<GetResourceGroupModCategoryRequest, ModifierCategoryModel>("GetModCategory"), GetMostRecentErrorEvents: CreatePostAction<GetResourceGroupLogRequest, AppLogEntryModel[]>("GetMostRecentErrorEvents"), GetMostRecentRequests: CreatePostAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]>("GetMostRecentRequests"), GetResourceGroup: CreatePostAction<GetResourceGroupRequest, ResourceGroupModel>("GetResourceGroup"), GetResources: CreatePostAction<GetResourcesRequest, ResourceModel[]>("GetResources"), GetRoleAccess: CreatePostAction<GetResourceGroupRoleAccessRequest, AppRoleModel[]>("GetRoleAccess"));
    }

    public ResourceGroupInquiryGroupActions Actions { get; }

    public Task<ModifierCategoryModel> GetModCategory(string modifier, GetResourceGroupModCategoryRequest model, CancellationToken ct = default) => Actions.GetModCategory.Post(modifier, model, ct);
    public Task<AppLogEntryModel[]> GetMostRecentErrorEvents(string modifier, GetResourceGroupLogRequest model, CancellationToken ct = default) => Actions.GetMostRecentErrorEvents.Post(modifier, model, ct);
    public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, GetResourceGroupLogRequest model, CancellationToken ct = default) => Actions.GetMostRecentRequests.Post(modifier, model, ct);
    public Task<ResourceGroupModel> GetResourceGroup(string modifier, GetResourceGroupRequest model, CancellationToken ct = default) => Actions.GetResourceGroup.Post(modifier, model, ct);
    public Task<ResourceModel[]> GetResources(string modifier, GetResourcesRequest model, CancellationToken ct = default) => Actions.GetResources.Post(modifier, model, ct);
    public Task<AppRoleModel[]> GetRoleAccess(string modifier, GetResourceGroupRoleAccessRequest model, CancellationToken ct = default) => Actions.GetRoleAccess.Post(modifier, model, ct);
    public sealed record ResourceGroupInquiryGroupActions(AppClientPostAction<GetResourceGroupModCategoryRequest, ModifierCategoryModel> GetModCategory, AppClientPostAction<GetResourceGroupLogRequest, AppLogEntryModel[]> GetMostRecentErrorEvents, AppClientPostAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests, AppClientPostAction<GetResourceGroupRequest, ResourceGroupModel> GetResourceGroup, AppClientPostAction<GetResourcesRequest, ResourceModel[]> GetResources, AppClientPostAction<GetResourceGroupRoleAccessRequest, AppRoleModel[]> GetRoleAccess);
}