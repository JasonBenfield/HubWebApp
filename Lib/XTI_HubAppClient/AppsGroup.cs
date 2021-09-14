// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class AppsGroup : AppClientGroup
    {
        public AppsGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "Apps")
        {
        }

        public Task<AppModel[]> All() => Post<AppModel[], EmptyRequest>("All", "", new EmptyRequest());
        public Task<string> GetAppModifierKey(AppKey model) => Post<string, AppKey>("GetAppModifierKey", "", model);
    }
}