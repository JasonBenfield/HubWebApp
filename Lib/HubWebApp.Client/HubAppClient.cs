// Generated Code
using XTI_WebAppClient;
using System.Net.Http;

namespace HubWebApp.client
{
    public sealed class HubAppClient : AppClient, IAuthClient
    {
        public HubAppClient(IHttpClientFactory httpClientFactory, XtiCredentials credentials, string baseUrl): base(httpClientFactory, baseUrl, "Hub")
        {
            xtiToken = new XtiToken(this, credentials);
            Auth = new AuthGroup(httpClientFactory, xtiToken, url);
            UserAdmin = new UserAdminGroup(httpClientFactory, xtiToken, url);
            Restricted = new RestrictedGroup(httpClientFactory, xtiToken, url);
        }

        public IAuthClientGroup Auth
        {
            get;
        }

        public UserAdminGroup UserAdmin
        {
            get;
        }

        public RestrictedGroup Restricted
        {
            get;
        }
    }
}