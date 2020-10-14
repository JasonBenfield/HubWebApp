// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace HubWebApp.client
{
    public sealed partial class AuthApiGroup : AppClientGroup, IAuthApiClientGroup
    {
        public AuthApiGroup(IHttpClientFactory httpClientFactory, XtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "AuthApi")
        {
        }

        public Task<LoginResult> Authenticate(LoginCredentials model) => Post<LoginResult, LoginCredentials>("Authenticate", "", model);
    }
}