// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class UserCacheGroup : AppClientGroup
    {
        public UserCacheGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "UserCache")
        {
        }

        public Task<EmptyActionResult> ClearCache(ClearUserCacheRequest model) => Post<EmptyActionResult, ClearUserCacheRequest>("ClearCache", "", model);
    }
}