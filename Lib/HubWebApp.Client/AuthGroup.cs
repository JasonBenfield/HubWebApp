// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace HubWebApp.client
{
    public sealed class AuthGroup : AppClientGroup, IAuthClientGroup
    {
        public AuthGroup(IHttpClientFactory httpClientFactory, XtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "Auth")
        {
        }

        public Task<LoginResult> Login(LoginModel model) => Post<LoginResult, LoginModel>("Login", model);
        public Task<LoginResult> Authenticate(LoginModel model) => Post<LoginResult, LoginModel>("Authenticate", model);
    }
}