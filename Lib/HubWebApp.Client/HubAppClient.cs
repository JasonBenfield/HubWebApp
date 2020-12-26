// Generated Code
using XTI_WebAppClient;
using System.Net.Http;

namespace HubWebApp.Client
{
    public sealed partial class HubAppClient : AppClient
    {
        public HubAppClient(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl, string version = DefaultVersion): base(httpClientFactory, baseUrl, "Hub", string.IsNullOrWhiteSpace(version) ? DefaultVersion : version)
        {
            this.xtiToken = xtiToken;
            User = new UserGroup(httpClientFactory, xtiToken, url);
            UserAdmin = new UserAdminGroup(httpClientFactory, xtiToken, url);
            Apps = new AppsGroup(httpClientFactory, xtiToken, url);
            App = new AppGroup(httpClientFactory, xtiToken, url);
        }

        public const string DefaultVersion = "V58";
        public UserGroup User
        {
            get;
        }

        public UserAdminGroup UserAdmin
        {
            get;
        }

        public AppsGroup Apps
        {
            get;
        }

        public AppGroup App
        {
            get;
        }
    }
}