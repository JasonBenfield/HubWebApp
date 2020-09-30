// Generated Code
using XTI_WebAppClient;
using System.Net.Http;

namespace HubWebApp.client
{
    public sealed class HubAppClient : AppClient, IAuthClient
    {
        public HubAppClient(IHttpClientFactory httpClientFactory, XtiCredentials credentials, string baseUrl, string version = "V0"): base(httpClientFactory, baseUrl, "Hub", version)
        {
            xtiToken = new XtiToken(this, credentials);
            Auth = new AuthGroup(httpClientFactory, xtiToken, url);
            UserAdmin = new UserAdminGroup(httpClientFactory, xtiToken, url);
        }

        public IAuthClientGroup Auth
        {
            get;
        }

        public UserAdminGroup UserAdmin
        {
            get;
        }
    }
}