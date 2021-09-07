// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class VersionGroup : AppClientGroup
    {
        public VersionGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "Version")
        {
        }

        public Task<AppVersionModel> GetVersion(string modifier, string model) => Post<AppVersionModel, string>("GetVersion", modifier, model);
        public Task<AppVersionModel> GetCurrentVersion(string modifier) => Post<AppVersionModel, EmptyRequest>("GetCurrentVersion", modifier, new EmptyRequest());
        public Task<ResourceGroupModel> GetResourceGroup(string modifier, GetVersionResourceGroupRequest model) => Post<ResourceGroupModel, GetVersionResourceGroupRequest>("GetResourceGroup", modifier, model);
    }
}