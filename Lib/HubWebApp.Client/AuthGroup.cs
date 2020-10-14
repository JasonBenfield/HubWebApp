// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace HubWebApp.client
{
    public sealed partial class AuthGroup : AppClientGroup
    {
        public AuthGroup(IHttpClientFactory httpClientFactory, XtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "Auth")
        {
        }

        public Task<EmptyActionResult> Verify(LoginCredentials model) => Post<EmptyActionResult, LoginCredentials>("Verify", "", model);
    }
}