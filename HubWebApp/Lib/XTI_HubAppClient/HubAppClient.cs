// Generated Code
namespace XTI_HubAppClient;
public sealed partial class HubAppClient : AppClient
{
    public HubAppClient(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, HubAppClientVersion version) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Hub", version.Value)
    {
        User = CreateGroup((_clientFactory, _tokenAccessor, _url) => new UserGroup(_clientFactory, _tokenAccessor, _url));
        UserCache = CreateGroup((_clientFactory, _tokenAccessor, _url) => new UserCacheGroup(_clientFactory, _tokenAccessor, _url));
        Home = CreateGroup((_clientFactory, _tokenAccessor, _url) => new HomeGroup(_clientFactory, _tokenAccessor, _url));
        Auth = CreateGroup((_clientFactory, _tokenAccessor, _url) => new AuthGroup(_clientFactory, _tokenAccessor, _url));
        AuthApi = CreateGroup((_clientFactory, _tokenAccessor, _url) => new AuthApiGroup(_clientFactory, _tokenAccessor, _url));
        ExternalAuth = CreateGroup((_clientFactory, _tokenAccessor, _url) => new ExternalAuthGroup(_clientFactory, _tokenAccessor, _url));
        Authenticators = CreateGroup((_clientFactory, _tokenAccessor, _url) => new AuthenticatorsGroup(_clientFactory, _tokenAccessor, _url));
        PermanentLog = CreateGroup((_clientFactory, _tokenAccessor, _url) => new PermanentLogGroup(_clientFactory, _tokenAccessor, _url));
        Apps = CreateGroup((_clientFactory, _tokenAccessor, _url) => new AppsGroup(_clientFactory, _tokenAccessor, _url));
        App = CreateGroup((_clientFactory, _tokenAccessor, _url) => new AppGroup(_clientFactory, _tokenAccessor, _url));
        Install = CreateGroup((_clientFactory, _tokenAccessor, _url) => new InstallGroup(_clientFactory, _tokenAccessor, _url));
        Publish = CreateGroup((_clientFactory, _tokenAccessor, _url) => new PublishGroup(_clientFactory, _tokenAccessor, _url));
        Version = CreateGroup((_clientFactory, _tokenAccessor, _url) => new VersionGroup(_clientFactory, _tokenAccessor, _url));
        ResourceGroup = CreateGroup((_clientFactory, _tokenAccessor, _url) => new ResourceGroupGroup(_clientFactory, _tokenAccessor, _url));
        Resource = CreateGroup((_clientFactory, _tokenAccessor, _url) => new ResourceGroup(_clientFactory, _tokenAccessor, _url));
        ModCategory = CreateGroup((_clientFactory, _tokenAccessor, _url) => new ModCategoryGroup(_clientFactory, _tokenAccessor, _url));
        Users = CreateGroup((_clientFactory, _tokenAccessor, _url) => new UsersGroup(_clientFactory, _tokenAccessor, _url));
        UserInquiry = CreateGroup((_clientFactory, _tokenAccessor, _url) => new UserInquiryGroup(_clientFactory, _tokenAccessor, _url));
        AppUser = CreateGroup((_clientFactory, _tokenAccessor, _url) => new AppUserGroup(_clientFactory, _tokenAccessor, _url));
        AppUserMaintenance = CreateGroup((_clientFactory, _tokenAccessor, _url) => new AppUserMaintenanceGroup(_clientFactory, _tokenAccessor, _url));
        UserMaintenance = CreateGroup((_clientFactory, _tokenAccessor, _url) => new UserMaintenanceGroup(_clientFactory, _tokenAccessor, _url));
        Storage = CreateGroup((_clientFactory, _tokenAccessor, _url) => new StorageGroup(_clientFactory, _tokenAccessor, _url));
        System = CreateGroup((_clientFactory, _tokenAccessor, _url) => new SystemGroup(_clientFactory, _tokenAccessor, _url));
        UserGroups = CreateGroup((_clientFactory, _tokenAccessor, _url) => new UserGroupsGroup(_clientFactory, _tokenAccessor, _url));
        UserQuery = CreateODataGroup<UserGroupKey, ExpandedUser>("UserQuery");
    }

    public HubRoleNames RoleNames { get; } = HubRoleNames.Instance;
    public string AppName { get; } = "Hub";
    public UserGroup User { get; }

    public UserCacheGroup UserCache { get; }

    public HomeGroup Home { get; }

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

    public StorageGroup Storage { get; }

    public SystemGroup System { get; }

    public UserGroupsGroup UserGroups { get; }

    public AppClientODataGroup<UserGroupKey, ExpandedUser> UserQuery { get; }
}