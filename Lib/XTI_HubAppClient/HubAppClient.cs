// Generated Code
using XTI_WebAppClient;
using System.Net.Http;

namespace XTI_HubAppClient
{
    public sealed partial class HubAppClient : AppClient
    {
        public HubAppClient(IHttpClientFactory httpClientFactory, IXtiTokenFactory tokenFactory, string baseUrl, string version = DefaultVersion): base(httpClientFactory, baseUrl, "Hub", string.IsNullOrWhiteSpace(version) ? DefaultVersion : version)
        {
            xtiToken = tokenFactory.Create(this);
            User = new UserGroup(httpClientFactory, xtiToken, url);
            UserCache = new UserCacheGroup(httpClientFactory, xtiToken, url);
            Auth = new AuthGroup(httpClientFactory, xtiToken, url);
            AuthApi = new AuthApiGroup(httpClientFactory, xtiToken, url);
            PermanentLog = new PermanentLogGroup(httpClientFactory, xtiToken, url);
            Apps = new AppsGroup(httpClientFactory, xtiToken, url);
            App = new AppGroup(httpClientFactory, xtiToken, url);
            Version = new VersionGroup(httpClientFactory, xtiToken, url);
            ResourceGroup = new ResourceGroupGroup(httpClientFactory, xtiToken, url);
            Resource = new ResourceGroup(httpClientFactory, xtiToken, url);
            ModCategory = new ModCategoryGroup(httpClientFactory, xtiToken, url);
            Users = new UsersGroup(httpClientFactory, xtiToken, url);
            UserInquiry = new UserInquiryGroup(httpClientFactory, xtiToken, url);
            AppUser = new AppUserGroup(httpClientFactory, xtiToken, url);
            AppUserMaintenance = new AppUserMaintenanceGroup(httpClientFactory, xtiToken, url);
            UserMaintenance = new UserMaintenanceGroup(httpClientFactory, xtiToken, url);
        }

        public const string DefaultVersion = "Current";
        public UserGroup User { get; }

        public UserCacheGroup UserCache { get; }

        public AuthGroup Auth { get; }

        public AuthApiGroup AuthApi { get; }

        public PermanentLogGroup PermanentLog { get; }

        public AppsGroup Apps { get; }

        public AppGroup App { get; }

        public VersionGroup Version { get; }

        public ResourceGroupGroup ResourceGroup { get; }

        public ResourceGroup Resource { get; }

        public ModCategoryGroup ModCategory { get; }

        public UsersGroup Users { get; }

        public UserInquiryGroup UserInquiry { get; }

        public AppUserGroup AppUser { get; }

        public AppUserMaintenanceGroup AppUserMaintenance { get; }

        public UserMaintenanceGroup UserMaintenance { get; }
    }
}