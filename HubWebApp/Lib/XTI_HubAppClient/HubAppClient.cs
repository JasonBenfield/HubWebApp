// Generated Code
namespace XTI_HubAppClient;
public sealed partial class HubAppClient : AppClient
{
    public HubAppClient(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, IAppClientRequestKey requestKey, HubAppClientVersion version) : base(httpClientFactory, xtiTokenAccessor, clientUrl, requestKey, "Hub", version.Value)
    {
        CurrentUser = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new CurrentUserGroup(_clientFactory, _tokenAccessor, _url, _options));
        Home = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new HomeGroup(_clientFactory, _tokenAccessor, _url, _options));
        Installations = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new InstallationsGroup(_clientFactory, _tokenAccessor, _url, _options));
        InstallationQuery = CreateODataGroup<InstallationQueryRequest, ExpandedInstallation>("InstallationQuery");
        Auth = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AuthGroup(_clientFactory, _tokenAccessor, _url, _options));
        AuthApi = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AuthApiGroup(_clientFactory, _tokenAccessor, _url, _options));
        ExternalAuth = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new ExternalAuthGroup(_clientFactory, _tokenAccessor, _url, _options));
        Authenticators = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AuthenticatorsGroup(_clientFactory, _tokenAccessor, _url, _options));
        PermanentLog = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new PermanentLogGroup(_clientFactory, _tokenAccessor, _url, _options));
        Apps = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AppsGroup(_clientFactory, _tokenAccessor, _url, _options));
        App = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AppGroup(_clientFactory, _tokenAccessor, _url, _options));
        Install = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new InstallGroup(_clientFactory, _tokenAccessor, _url, _options));
        Publish = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new PublishGroup(_clientFactory, _tokenAccessor, _url, _options));
        Version = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new VersionGroup(_clientFactory, _tokenAccessor, _url, _options));
        ResourceGroup = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new ResourceGroupGroup(_clientFactory, _tokenAccessor, _url, _options));
        Resource = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new ResourceGroup(_clientFactory, _tokenAccessor, _url, _options));
        ModCategory = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new ModCategoryGroup(_clientFactory, _tokenAccessor, _url, _options));
        Users = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new UsersGroup(_clientFactory, _tokenAccessor, _url, _options));
        UserInquiry = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new UserInquiryGroup(_clientFactory, _tokenAccessor, _url, _options));
        AppUser = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AppUserGroup(_clientFactory, _tokenAccessor, _url, _options));
        AppUserMaintenance = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AppUserMaintenanceGroup(_clientFactory, _tokenAccessor, _url, _options));
        UserMaintenance = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new UserMaintenanceGroup(_clientFactory, _tokenAccessor, _url, _options));
        Storage = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new StorageGroup(_clientFactory, _tokenAccessor, _url, _options));
        System = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new SystemGroup(_clientFactory, _tokenAccessor, _url, _options));
        UserGroups = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new UserGroupsGroup(_clientFactory, _tokenAccessor, _url, _options));
        UserQuery = CreateODataGroup<UserGroupKey, ExpandedUser>("UserQuery");
        Periodic = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new PeriodicGroup(_clientFactory, _tokenAccessor, _url, _options));
        Logs = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new LogsGroup(_clientFactory, _tokenAccessor, _url, _options));
        SessionQuery = CreateODataGroup<EmptyRequest, ExpandedSession>("SessionQuery");
        RequestQuery = CreateODataGroup<AppRequestQueryRequest, ExpandedRequest>("RequestQuery");
        LogEntryQuery = CreateODataGroup<LogEntryQueryRequest, ExpandedLogEntry>("LogEntryQuery");
    }

    public HubRoleNames RoleNames { get; } = HubRoleNames.Instance;
    public string AppName { get; } = "Hub";
    public CurrentUserGroup CurrentUser { get; }
    public HomeGroup Home { get; }
    public InstallationsGroup Installations { get; }
    public AppClientODataGroup<InstallationQueryRequest, ExpandedInstallation> InstallationQuery { get; }
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
    public PeriodicGroup Periodic { get; }
    public LogsGroup Logs { get; }
    public AppClientODataGroup<EmptyRequest, ExpandedSession> SessionQuery { get; }
    public AppClientODataGroup<AppRequestQueryRequest, ExpandedRequest> RequestQuery { get; }
    public AppClientODataGroup<LogEntryQueryRequest, ExpandedLogEntry> LogEntryQuery { get; }
}