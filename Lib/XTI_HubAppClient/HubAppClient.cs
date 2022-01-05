// Generated Code
namespace XTI_HubAppClient;
public sealed partial class HubAppClient : AppClient
{
    public const string DefaultVersion = "V1169";
    public HubAppClient(IHttpClientFactory httpClientFactory, IXtiTokenFactory tokenFactory, AppClientUrl clientUrl, string version = DefaultVersion) : base(httpClientFactory, clientUrl, "Hub", string.IsNullOrWhiteSpace(version) ? DefaultVersion : version)
    {
        xtiToken = tokenFactory.Create(this);
        User = GetGroup((_clientFactory, _token, _url) => new UserGroup(_clientFactory, _token, _url));
        UserCache = GetGroup((_clientFactory, _token, _url) => new UserCacheGroup(_clientFactory, _token, _url));
        Auth = GetGroup((_clientFactory, _token, _url) => new AuthGroup(_clientFactory, _token, _url));
        AuthApi = GetGroup((_clientFactory, _token, _url) => new AuthApiGroup(_clientFactory, _token, _url));
        ExternalAuth = GetGroup((_clientFactory, _token, _url) => new ExternalAuthGroup(_clientFactory, _token, _url));
        Authenticators = GetGroup((_clientFactory, _token, _url) => new AuthenticatorsGroup(_clientFactory, _token, _url));
        PermanentLog = GetGroup((_clientFactory, _token, _url) => new PermanentLogGroup(_clientFactory, _token, _url));
        Apps = GetGroup((_clientFactory, _token, _url) => new AppsGroup(_clientFactory, _token, _url));
        App = GetGroup((_clientFactory, _token, _url) => new AppGroup(_clientFactory, _token, _url));
        Install = GetGroup((_clientFactory, _token, _url) => new InstallGroup(_clientFactory, _token, _url));
        Publish = GetGroup((_clientFactory, _token, _url) => new PublishGroup(_clientFactory, _token, _url));
        Version = GetGroup((_clientFactory, _token, _url) => new VersionGroup(_clientFactory, _token, _url));
        ResourceGroup = GetGroup((_clientFactory, _token, _url) => new ResourceGroupGroup(_clientFactory, _token, _url));
        Resource = GetGroup((_clientFactory, _token, _url) => new ResourceGroup(_clientFactory, _token, _url));
        ModCategory = GetGroup((_clientFactory, _token, _url) => new ModCategoryGroup(_clientFactory, _token, _url));
        Users = GetGroup((_clientFactory, _token, _url) => new UsersGroup(_clientFactory, _token, _url));
        UserInquiry = GetGroup((_clientFactory, _token, _url) => new UserInquiryGroup(_clientFactory, _token, _url));
        AppUser = GetGroup((_clientFactory, _token, _url) => new AppUserGroup(_clientFactory, _token, _url));
        AppUserMaintenance = GetGroup((_clientFactory, _token, _url) => new AppUserMaintenanceGroup(_clientFactory, _token, _url));
        UserMaintenance = GetGroup((_clientFactory, _token, _url) => new UserMaintenanceGroup(_clientFactory, _token, _url));
    }

    public UserGroup User { get; }

    public UserCacheGroup UserCache { get; }

    public AuthGroup Auth { get; }

    public AuthApiGroup AuthApi { get; }

    public ExternalAuthGroup ExternalAuth { get; }

    public AuthenticatorsGroup Authenticators { get; }

    public PermanentLogGroup PermanentLog { get; }

    public AppsGroup Apps { get; }

    public AppGroup App { get; }

    public InstallGroup Install { get; }

    public PublishGroup Publish { get; }

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