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
            ResourceGroup = new ResourceGroupGroup(httpClientFactory, xtiToken, url);
            Resource = new ResourceGroup(httpClientFactory, xtiToken, url);
            ModCategory = new ModCategoryGroup(httpClientFactory, xtiToken, url);
            Users = new UsersGroup(httpClientFactory, xtiToken, url);
        }

        public const string DefaultVersion = "V21";
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

        public ResourceGroupGroup ResourceGroup
        {
            get;
        }

        public ResourceGroup Resource
        {
            get;
        }

        public ModCategoryGroup ModCategory
        {
            get;
        }

        public UsersGroup Users
        {
            get;
        }
    }
}