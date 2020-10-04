// Generated Code
using XTI_WebAppClient;
using System.Net.Http;

namespace HubWebApp.client
{
    public sealed partial class HubAppClient : AppClient, IAuthClient
    {
        public HubAppClient(IHttpClientFactory httpClientFactory, XtiCredentials credentials, string baseUrl, string version = "V2"): base(httpClientFactory, baseUrl, "Hub", version)
        {
            xtiToken = new XtiToken(this, credentials);
            Auth = new AuthGroup(httpClientFactory, xtiToken, url);
            AuthApi = new AuthApiGroup(httpClientFactory, xtiToken, url);
            UserAdmin = new UserAdminGroup(httpClientFactory, xtiToken, url);
        }

        public AuthGroup Auth
        {
            get;
        }

        public IAuthApiClientGroup AuthApi
        {
            get;
        }

        public UserAdminGroup UserAdmin
        {
            get;
        }
    }
}