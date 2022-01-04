// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ResourceGroupGroup : AppClientGroup
{
    public ResourceGroupGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, AppClientUrl clientUrl) : base(httpClientFactory, xtiToken, clientUrl, "ResourceGroup")
    {
    }

    public Task<ResourceGroupModel> GetResourceGroup(string modifier, GetResourceGroupRequest model) => Post<ResourceGroupModel, GetResourceGroupRequest>("GetResourceGroup", modifier, model);
    public Task<ResourceModel[]> GetResources(string modifier, GetResourcesRequest model) => Post<ResourceModel[], GetResourcesRequest>("GetResources", modifier, model);
    public Task<ResourceModel> GetResource(string modifier, GetResourceGroupResourceRequest model) => Post<ResourceModel, GetResourceGroupResourceRequest>("GetResource", modifier, model);
    public Task<AppRoleModel[]> GetRoleAccess(string modifier, GetResourceGroupRoleAccessRequest model) => Post<AppRoleModel[], GetResourceGroupRoleAccessRequest>("GetRoleAccess", modifier, model);
    public Task<ModifierCategoryModel> GetModCategory(string modifier, GetResourceGroupModCategoryRequest model) => Post<ModifierCategoryModel, GetResourceGroupModCategoryRequest>("GetModCategory", modifier, model);
    public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, GetResourceGroupLogRequest model) => Post<AppRequestExpandedModel[], GetResourceGroupLogRequest>("GetMostRecentRequests", modifier, model);
    public Task<AppEventModel[]> GetMostRecentErrorEvents(string modifier, GetResourceGroupLogRequest model) => Post<AppEventModel[], GetResourceGroupLogRequest>("GetMostRecentErrorEvents", modifier, model);
}