using XTI_HubWebAppApi.App;
using XTI_HubWebAppApi.Apps;
using XTI_HubWebAppApi.AppUserInquiry;
using XTI_HubWebAppApi.AppUserMaintenance;
using XTI_HubWebAppApi.Auth;
using XTI_HubWebAppApi.AuthApi;
using XTI_HubWebAppApi.Authenticators;
using XTI_HubWebAppApi.CurrentUser;
using XTI_HubWebAppApi.ExternalAuth;
using XTI_HubWebAppApi.Home;
using XTI_HubWebAppApi.Install;
using XTI_HubWebAppApi.Installations;
using XTI_HubWebAppApi.Logs;
using XTI_HubWebAppApi.ModCategory;
using XTI_HubWebAppApi.Periodic;
using XTI_HubWebAppApi.PermanentLog;
using XTI_HubWebAppApi.Publish;
using XTI_HubWebAppApi.ResourceGroupInquiry;
using XTI_HubWebAppApi.ResourceInquiry;
using XTI_HubWebAppApi.Storage;
using XTI_HubWebAppApi.System;
using XTI_HubWebAppApi.UserGroups;
using XTI_HubWebAppApi.UserInquiry;
using XTI_HubWebAppApi.UserMaintenance;
using XTI_HubWebAppApi.UserRoles;
using XTI_HubWebAppApi.Users;
using XTI_HubWebAppApi.Version;
using XTI_ODataQuery.Api;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi;
public sealed partial class HubAppApiBuilder
{
    private readonly AppApi source;
    private readonly IServiceProvider sp;
    public HubAppApiBuilder(IServiceProvider sp, IAppApiUser user)
    {
        source = new AppApi(sp, HubAppKey.Value, user);
        this.sp = sp;
        App = new AppGroupBuilder(source.AddGroup("App"));
        Apps = new AppsGroupBuilder(source.AddGroup("Apps"));
        AppUserInquiry = new AppUserInquiryGroupBuilder(source.AddGroup("AppUserInquiry"));
        AppUserMaintenance = new AppUserMaintenanceGroupBuilder(source.AddGroup("AppUserMaintenance"));
        Auth = new AuthGroupBuilder(source.AddGroup("Auth"));
        AuthApi = new AuthApiGroupBuilder(source.AddGroup("AuthApi"));
        Authenticators = new AuthenticatorsGroupBuilder(source.AddGroup("Authenticators"));
        CurrentUser = new CurrentUserGroupBuilder(source.AddGroup("CurrentUser"));
        ExternalAuth = new ExternalAuthGroupBuilder(source.AddGroup("ExternalAuth"));
        Home = new HomeGroupBuilder(source.AddGroup("Home"));
        Install = new InstallGroupBuilder(source.AddGroup("Install"));
        Installations = new InstallationsGroupBuilder(source.AddGroup("Installations"));
        Logs = new LogsGroupBuilder(source.AddGroup("Logs"));
        ModCategory = new ModCategoryGroupBuilder(source.AddGroup("ModCategory"));
        Periodic = new PeriodicGroupBuilder(source.AddGroup("Periodic"));
        PermanentLog = new PermanentLogGroupBuilder(source.AddGroup("PermanentLog"));
        Publish = new PublishGroupBuilder(source.AddGroup("Publish"));
        ResourceGroupInquiry = new ResourceGroupInquiryGroupBuilder(source.AddGroup("ResourceGroupInquiry"));
        ResourceInquiry = new ResourceInquiryGroupBuilder(source.AddGroup("ResourceInquiry"));
        Storage = new StorageGroupBuilder(source.AddGroup("Storage"));
        System = new SystemGroupBuilder(source.AddGroup("System"));
        UserGroups = new UserGroupsGroupBuilder(source.AddGroup("UserGroups"));
        UserInquiry = new UserInquiryGroupBuilder(source.AddGroup("UserInquiry"));
        UserMaintenance = new UserMaintenanceGroupBuilder(source.AddGroup("UserMaintenance"));
        UserRoles = new UserRolesGroupBuilder(source.AddGroup("UserRoles"));
        Users = new UsersGroupBuilder(source.AddGroup("Users"));
        Version = new VersionGroupBuilder(source.AddGroup("Version"));
        InstallationQuery = new ODataGroupBuilder<InstallationQueryRequest, ExpandedInstallation>(source.AddGroup("InstallationQuery")).WithQuery<XTI_HubWebAppApiActions.Installations.InstallationQueryAction>();
        AppRequestQuery = new ODataGroupBuilder<AppRequestQueryRequest, ExpandedRequest>(source.AddGroup("AppRequestQuery")).WithQuery<XTI_HubWebAppApiActions.Logs.AppRequestQueryAction>();
        LogEntryQuery = new ODataGroupBuilder<LogEntryQueryRequest, ExpandedLogEntry>(source.AddGroup("LogEntryQuery")).WithQuery<XTI_HubWebAppApiActions.Logs.LogEntryQueryAction>();
        SessionQuery = new ODataGroupBuilder<EmptyRequest, ExpandedSession>(source.AddGroup("SessionQuery")).WithQuery<XTI_HubWebAppApiActions.Logs.SessionQueryAction>();
        UserQuery = new ODataGroupBuilder<UserGroupKey, ExpandedUser>(source.AddGroup("UserQuery")).WithQuery<XTI_HubWebAppApiActions.UserGroups.UserQueryAction>();
        UserRoleQuery = new ODataGroupBuilder<UserRoleQueryRequest, ExpandedUserRole>(source.AddGroup("UserRoleQuery")).WithQuery<XTI_HubWebAppApiActions.UserRoles.UserRoleQueryAction>();
        Configure();
    }

    partial void Configure();
    public AppGroupBuilder App { get; }
    public AppsGroupBuilder Apps { get; }
    public AppUserInquiryGroupBuilder AppUserInquiry { get; }
    public AppUserMaintenanceGroupBuilder AppUserMaintenance { get; }
    public AuthGroupBuilder Auth { get; }
    public AuthApiGroupBuilder AuthApi { get; }
    public AuthenticatorsGroupBuilder Authenticators { get; }
    public CurrentUserGroupBuilder CurrentUser { get; }
    public ExternalAuthGroupBuilder ExternalAuth { get; }
    public HomeGroupBuilder Home { get; }
    public InstallGroupBuilder Install { get; }
    public InstallationsGroupBuilder Installations { get; }
    public LogsGroupBuilder Logs { get; }
    public ModCategoryGroupBuilder ModCategory { get; }
    public PeriodicGroupBuilder Periodic { get; }
    public PermanentLogGroupBuilder PermanentLog { get; }
    public PublishGroupBuilder Publish { get; }
    public ResourceGroupInquiryGroupBuilder ResourceGroupInquiry { get; }
    public ResourceInquiryGroupBuilder ResourceInquiry { get; }
    public StorageGroupBuilder Storage { get; }
    public SystemGroupBuilder System { get; }
    public UserGroupsGroupBuilder UserGroups { get; }
    public UserInquiryGroupBuilder UserInquiry { get; }
    public UserMaintenanceGroupBuilder UserMaintenance { get; }
    public UserRolesGroupBuilder UserRoles { get; }
    public UsersGroupBuilder Users { get; }
    public VersionGroupBuilder Version { get; }
    public ODataGroupBuilder<InstallationQueryRequest, ExpandedInstallation> InstallationQuery { get; }
    public ODataGroupBuilder<AppRequestQueryRequest, ExpandedRequest> AppRequestQuery { get; }
    public ODataGroupBuilder<LogEntryQueryRequest, ExpandedLogEntry> LogEntryQuery { get; }
    public ODataGroupBuilder<EmptyRequest, ExpandedSession> SessionQuery { get; }
    public ODataGroupBuilder<UserGroupKey, ExpandedUser> UserQuery { get; }
    public ODataGroupBuilder<UserRoleQueryRequest, ExpandedUserRole> UserRoleQuery { get; }

    public HubAppApi Build() => new HubAppApi(source, this);
}