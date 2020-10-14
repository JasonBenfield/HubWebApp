// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using XTI_Credentials;

namespace HubWebApp.client
{
    public sealed partial class HubAppClient : AppClient, IAuthClient
    {
        public HubAppClient(IHttpClientFactory httpClientFactory, ICredentials credentials, string baseUrl, string version = "V2"): base(httpClientFactory, baseUrl, "Hub", version)
        {
            xtiToken = new XtiToken(this, credentials);
            User = new UserGroup(httpClientFactory, xtiToken, url);
            Auth = new AuthGroup(httpClientFactory, xtiToken, url);
            AuthApi = new AuthApiGroup(httpClientFactory, xtiToken, url);
            UserAdmin = new UserAdminGroup(httpClientFactory, xtiToken, url);
        }

        public UserGroup User
        {
            get;
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