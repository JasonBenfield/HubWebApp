// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ResourceGroupGroup : AppClientGroup
{
    public ResourceGroupGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "ResourceGroup")
    {
        Actions = new ResourceGroupGroupActions(GetResourceGroup: CreatePostAction<GetResourceGroupRequest, ResourceGroupModel>("GetResourceGroup"), GetResources: CreatePostAction<GetResourcesRequest, ResourceModel[]>("GetResources"), GetRoleAccess: CreatePostAction<GetResourceGroupRoleAccessRequest, AppRoleModel[]>("GetRoleAccess"), GetModCategory: CreatePostAction<GetResourceGroupModCategoryRequest, ModifierCategoryModel>("GetModCategory"), GetMostRecentRequests: CreatePostAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]>("GetMostRecentRequests"), GetMostRecentErrorEvents: CreatePostAction<GetResourceGroupLogRequest, AppLogEntryModel[]>("GetMostRecentErrorEvents"));
    }

    public ResourceGroupGroupActions Actions { get; }

    public Task<ResourceGroupModel> GetResourceGroup(string modifier, GetResourceGroupRequest model, CancellationToken ct = default) => Actions.GetResourceGroup.Post(modifier, model, ct);
    public Task<ResourceModel[]> GetResources(string modifier, GetResourcesRequest model, CancellationToken ct = default) => Actions.GetResources.Post(modifier, model, ct);
    public Task<AppRoleModel[]> GetRoleAccess(string modifier, GetResourceGroupRoleAccessRequest model, CancellationToken ct = default) => Actions.GetRoleAccess.Post(modifier, model, ct);
    public Task<ModifierCategoryModel> GetModCategory(string modifier, GetResourceGroupModCategoryRequest model, CancellationToken ct = default) => Actions.GetModCategory.Post(modifier, model, ct);
    public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, GetResourceGroupLogRequest model, CancellationToken ct = default) => Actions.GetMostRecentRequests.Post(modifier, model, ct);
    public Task<AppLogEntryModel[]> GetMostRecentErrorEvents(string modifier, GetResourceGroupLogRequest model, CancellationToken ct = default) => Actions.GetMostRecentErrorEvents.Post(modifier, model, ct);
    public sealed record ResourceGroupGroupActions(AppClientPostAction<GetResourceGroupRequest, ResourceGroupModel> GetResourceGroup, AppClientPostAction<GetResourcesRequest, ResourceModel[]> GetResources, AppClientPostAction<GetResourceGroupRoleAccessRequest, AppRoleModel[]> GetRoleAccess, AppClientPostAction<GetResourceGroupModCategoryRequest, ModifierCategoryModel> GetModCategory, AppClientPostAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests, AppClientPostAction<GetResourceGroupLogRequest, AppLogEntryModel[]> GetMostRecentErrorEvents);
}