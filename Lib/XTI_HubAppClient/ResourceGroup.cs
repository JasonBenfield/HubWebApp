// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class ResourceGroup : AppClientGroup
    {
        public ResourceGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "Resource")
        {
        }

        public Task<ResourceModel> GetResource(string modifier, GetResourceRequest model) => Post<ResourceModel, GetResourceRequest>("GetResource", modifier, model);
        public Task<AppRoleModel[]> GetRoleAccess(string modifier, GetResourceRoleAccessRequest model) => Post<AppRoleModel[], GetResourceRoleAccessRequest>("GetRoleAccess", modifier, model);
        public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, GetResourceLogRequest model) => Post<AppRequestExpandedModel[], GetResourceLogRequest>("GetMostRecentRequests", modifier, model);
        public Task<AppEventModel[]> GetMostRecentErrorEvents(string modifier, GetResourceLogRequest model) => Post<AppEventModel[], GetResourceLogRequest>("GetMostRecentErrorEvents", modifier, model);
    }
}