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
public sealed partial class HubAppApi : WebAppApiWrapper
{
    internal HubAppApi(AppApi source, HubAppApiBuilder builder) : base(source)
    {
        App = builder.App.Build();
        Apps = builder.Apps.Build();
        AppUserInquiry = builder.AppUserInquiry.Build();
        AppUserMaintenance = builder.AppUserMaintenance.Build();
        Auth = builder.Auth.Build();
        AuthApi = builder.AuthApi.Build();
        Authenticators = builder.Authenticators.Build();
        CurrentUser = builder.CurrentUser.Build();
        ExternalAuth = builder.ExternalAuth.Build();
        Home = builder.Home.Build();
        Install = builder.Install.Build();
        Installations = builder.Installations.Build();
        Logs = builder.Logs.Build();
        ModCategory = builder.ModCategory.Build();
        Periodic = builder.Periodic.Build();
        PermanentLog = builder.PermanentLog.Build();
        Publish = builder.Publish.Build();
        ResourceGroupInquiry = builder.ResourceGroupInquiry.Build();
        ResourceInquiry = builder.ResourceInquiry.Build();
        Storage = builder.Storage.Build();
        System = builder.System.Build();
        UserGroups = builder.UserGroups.Build();
        UserInquiry = builder.UserInquiry.Build();
        UserMaintenance = builder.UserMaintenance.Build();
        UserRoles = builder.UserRoles.Build();
        Users = builder.Users.Build();
        Version = builder.Version.Build();
        InstallationQuery = builder.InstallationQuery.Build();
        AppRequestQuery = builder.AppRequestQuery.Build();
        LogEntryQuery = builder.LogEntryQuery.Build();
        SessionQuery = builder.SessionQuery.Build();
        UserQuery = builder.UserQuery.Build();
        UserRoleQuery = builder.UserRoleQuery.Build();
        Configure();
    }

    partial void Configure();
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
    public ODataGroup<InstallationQueryRequest, ExpandedInstallation> InstallationQuery { get; }
    public ODataGroup<AppRequestQueryRequest, ExpandedRequest> AppRequestQuery { get; }
    public ODataGroup<LogEntryQueryRequest, ExpandedLogEntry> LogEntryQuery { get; }
    public ODataGroup<EmptyRequest, ExpandedSession> SessionQuery { get; }
    public ODataGroup<UserGroupKey, ExpandedUser> UserQuery { get; }
    public ODataGroup<UserRoleQueryRequest, ExpandedUserRole> UserRoleQuery { get; }
}