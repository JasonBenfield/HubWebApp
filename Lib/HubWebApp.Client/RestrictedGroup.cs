// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace HubWebApp.client
{
    public sealed class RestrictedGroup : AppClientGroup
    {
        public RestrictedGroup(IHttpClientFactory httpClientFactory, XtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "Restricted")
        {
        }
    }
}