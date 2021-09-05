// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class ResourceGroupGroup : AppClientGroup
    {
        public ResourceGroupGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "ResourceGroup")
        {
        }

        public Task<ResourceGroupModel> GetResourceGroup(string modifier, int model) => Post<ResourceGroupModel, int>("GetResourceGroup", modifier, model);
        public Task<ResourceModel[]> GetResources(string modifier, int model) => Post<ResourceModel[], int>("GetResources", modifier, model);
        public Task<AppRoleModel[]> GetRoleAccess(string modifier, int model) => Post<AppRoleModel[], int>("GetRoleAccess", modifier, model);
        public Task<ModifierCategoryModel> GetModCategory(string modifier, int model) => Post<ModifierCategoryModel, int>("GetModCategory", modifier, model);
        public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, GetResourceGroupLogRequest model) => Post<AppRequestExpandedModel[], GetResourceGroupLogRequest>("GetMostRecentRequests", modifier, model);
        public Task<AppEventModel[]> GetMostRecentErrorEvents(string modifier, GetResourceGroupLogRequest model) => Post<AppEventModel[], GetResourceGroupLogRequest>("GetMostRecentErrorEvents", modifier, model);
    }
}