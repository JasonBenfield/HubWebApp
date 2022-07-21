// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ResourceGroupGroup : AppClientGroup
{
    public ResourceGroupGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "ResourceGroup")
    {
        Actions = new ResourceGroupGroupActions(GetResourceGroup: CreatePostAction<GetResourceGroupRequest, ResourceGroupModel>("GetResourceGroup"), GetResources: CreatePostAction<GetResourcesRequest, ResourceModel[]>("GetResources"), GetRoleAccess: CreatePostAction<GetResourceGroupRoleAccessRequest, AppRoleModel[]>("GetRoleAccess"), GetModCategory: CreatePostAction<GetResourceGroupModCategoryRequest, ModifierCategoryModel>("GetModCategory"), GetMostRecentRequests: CreatePostAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]>("GetMostRecentRequests"), GetMostRecentErrorEvents: CreatePostAction<GetResourceGroupLogRequest, AppLogEntryModel[]>("GetMostRecentErrorEvents"));
    }

    public ResourceGroupGroupActions Actions { get; }

    public Task<ResourceGroupModel> GetResourceGroup(string modifier, GetResourceGroupRequest model) => Actions.GetResourceGroup.Post(modifier, model);
    public Task<ResourceModel[]> GetResources(string modifier, GetResourcesRequest model) => Actions.GetResources.Post(modifier, model);
    public Task<AppRoleModel[]> GetRoleAccess(string modifier, GetResourceGroupRoleAccessRequest model) => Actions.GetRoleAccess.Post(modifier, model);
    public Task<ModifierCategoryModel> GetModCategory(string modifier, GetResourceGroupModCategoryRequest model) => Actions.GetModCategory.Post(modifier, model);
    public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, GetResourceGroupLogRequest model) => Actions.GetMostRecentRequests.Post(modifier, model);
    public Task<AppLogEntryModel[]> GetMostRecentErrorEvents(string modifier, GetResourceGroupLogRequest model) => Actions.GetMostRecentErrorEvents.Post(modifier, model);
    public sealed record ResourceGroupGroupActions(AppClientPostAction<GetResourceGroupRequest, ResourceGroupModel> GetResourceGroup, AppClientPostAction<GetResourcesRequest, ResourceModel[]> GetResources, AppClientPostAction<GetResourceGroupRoleAccessRequest, AppRoleModel[]> GetRoleAccess, AppClientPostAction<GetResourceGroupModCategoryRequest, ModifierCategoryModel> GetModCategory, AppClientPostAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests, AppClientPostAction<GetResourceGroupLogRequest, AppLogEntryModel[]> GetMostRecentErrorEvents);
}