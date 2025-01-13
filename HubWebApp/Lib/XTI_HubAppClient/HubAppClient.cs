// Generated Code
namespace XTI_HubAppClient;
public sealed partial class HubAppClient : AppClient
{
    public HubAppClient(IHttpClientFactory httpClientFactory, XtiTokenAccessorFactory xtiTokenAccessorFactory, AppClientUrl clientUrl, IAppClientSessionKey sessionKey, IAppClientRequestKey requestKey, HubAppClientVersion version) : base(httpClientFactory, xtiTokenAccessorFactory, clientUrl, sessionKey, requestKey, "Hub", version.Value)
    {
        App = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AppGroup(_clientFactory, _tokenAccessor, _url, _options));
        Apps = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AppsGroup(_clientFactory, _tokenAccessor, _url, _options));
        AppUserInquiry = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AppUserInquiryGroup(_clientFactory, _tokenAccessor, _url, _options));
        AppUserMaintenance = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AppUserMaintenanceGroup(_clientFactory, _tokenAccessor, _url, _options));
        Auth = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AuthGroup(_clientFactory, _tokenAccessor, _url, _options));
        AuthApi = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AuthApiGroup(_clientFactory, _tokenAccessor, _url, _options));
        Authenticators = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new AuthenticatorsGroup(_clientFactory, _tokenAccessor, _url, _options));
        CurrentUser = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new CurrentUserGroup(_clientFactory, _tokenAccessor, _url, _options));
        ExternalAuth = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new ExternalAuthGroup(_clientFactory, _tokenAccessor, _url, _options));
        Home = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new HomeGroup(_clientFactory, _tokenAccessor, _url, _options));
        Install = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new InstallGroup(_clientFactory, _tokenAccessor, _url, _options));
        Installations = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new InstallationsGroup(_clientFactory, _tokenAccessor, _url, _options));
        Logs = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new LogsGroup(_clientFactory, _tokenAccessor, _url, _options));
        ModCategory = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new ModCategoryGroup(_clientFactory, _tokenAccessor, _url, _options));
        Periodic = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new PeriodicGroup(_clientFactory, _tokenAccessor, _url, _options));
        PermanentLog = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new PermanentLogGroup(_clientFactory, _tokenAccessor, _url, _options));
        Publish = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new PublishGroup(_clientFactory, _tokenAccessor, _url, _options));
        ResourceGroupInquiry = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new ResourceGroupInquiryGroup(_clientFactory, _tokenAccessor, _url, _options));
        ResourceInquiry = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new ResourceInquiryGroup(_clientFactory, _tokenAccessor, _url, _options));
        Storage = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new StorageGroup(_clientFactory, _tokenAccessor, _url, _options));
        System = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new SystemGroup(_clientFactory, _tokenAccessor, _url, _options));
        UserGroups = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new UserGroupsGroup(_clientFactory, _tokenAccessor, _url, _options));
        UserInquiry = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new UserInquiryGroup(_clientFactory, _tokenAccessor, _url, _options));
        UserMaintenance = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new UserMaintenanceGroup(_clientFactory, _tokenAccessor, _url, _options));
        UserRoles = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new UserRolesGroup(_clientFactory, _tokenAccessor, _url, _options));
        Users = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new UsersGroup(_clientFactory, _tokenAccessor, _url, _options));
        Version = CreateGroup((_clientFactory, _tokenAccessor, _url, _options) => new VersionGroup(_clientFactory, _tokenAccessor, _url, _options));
        InstallationQuery = CreateODataGroup<InstallationQueryRequest, ExpandedInstallation>("InstallationQuery");
        AppRequestQuery = CreateODataGroup<AppRequestQueryRequest, ExpandedRequest>("AppRequestQuery");
        LogEntryQuery = CreateODataGroup<LogEntryQueryRequest, ExpandedLogEntry>("LogEntryQuery");
        SessionQuery = CreateODataGroup<EmptyRequest, ExpandedSession>("SessionQuery");
        UserQuery = CreateODataGroup<UserGroupKey, ExpandedUser>("UserQuery");
        UserRoleQuery = CreateODataGroup<UserRoleQueryRequest, ExpandedUserRole>("UserRoleQuery");
        Configure();
    }

    partial void Configure();
    public HubRoleNames RoleNames { get; } = HubRoleNames.Instance;
    public string AppName { get; } = "Hub";
    public AppGroup App { get; }
    public AppsGroup Apps { get; }
    public AppUserInquiryGroup AppUserInquiry { get; }
    public AppUserMaintenanceGroup AppUserMaintenance { get; }
    public AuthGroup Auth { get; }
    public AuthApiGroup AuthApi { get; }
    public AuthenticatorsGroup Authenticators { get; }
    public CurrentUserGroup CurrentUser { get; }
    public ExternalAuthGroup ExternalAuth { get; }
    public HomeGroup Home { get; }
    public InstallGroup Install { get; }
    public InstallationsGroup Installations { get; }
    public LogsGroup Logs { get; }
    public ModCategoryGroup ModCategory { get; }
    public PeriodicGroup Periodic { get; }
    public PermanentLogGroup PermanentLog { get; }
    public PublishGroup Publish { get; }
    public ResourceGroupInquiryGroup ResourceGroupInquiry { get; }
    public ResourceInquiryGroup ResourceInquiry { get; }
    public StorageGroup Storage { get; }
    public SystemGroup System { get; }
    public UserGroupsGroup UserGroups { get; }
    public UserInquiryGroup UserInquiry { get; }
    public UserMaintenanceGroup UserMaintenance { get; }
    public UserRolesGroup UserRoles { get; }
    public UsersGroup Users { get; }
    public VersionGroup Version { get; }
    public AppClientODataGroup<InstallationQueryRequest, ExpandedInstallation> InstallationQuery { get; }
    public AppClientODataGroup<AppRequestQueryRequest, ExpandedRequest> AppRequestQuery { get; }
    public AppClientODataGroup<LogEntryQueryRequest, ExpandedLogEntry> LogEntryQuery { get; }
    public AppClientODataGroup<EmptyRequest, ExpandedSession> SessionQuery { get; }
    public AppClientODataGroup<UserGroupKey, ExpandedUser> UserQuery { get; }
    public AppClientODataGroup<UserRoleQueryRequest, ExpandedUserRole> UserRoleQuery { get; }
}