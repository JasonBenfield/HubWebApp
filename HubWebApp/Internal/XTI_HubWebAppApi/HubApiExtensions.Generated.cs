// Generated Code
namespace XTI_HubWebAppApi;
public static partial class HubApiExtensions
{
    public static void AddHubApiServices(this IServiceCollection services)
    {
        services.AddAppServices();
        services.AddAppsServices();
        services.AddAppUserInquiryServices();
        services.AddAppUserMaintenanceServices();
        services.AddAuthServices();
        services.AddAuthApiServices();
        services.AddAuthenticatorsServices();
        services.AddCurrentUserServices();
        services.AddExternalAuthServices();
        services.AddHomeServices();
        services.AddInstallServices();
        services.AddInstallationsServices();
        services.AddLogsServices();
        services.AddModCategoryServices();
        services.AddPeriodicServices();
        services.AddPermanentLogServices();
        services.AddPublishServices();
        services.AddResourceGroupInquiryServices();
        services.AddResourceInquiryServices();
        services.AddStorageServices();
        services.AddSystemServices();
        services.AddUserGroupsServices();
        services.AddUserInquiryServices();
        services.AddUserMaintenanceServices();
        services.AddUserRolesServices();
        services.AddUsersServices();
        services.AddVersionServices();
        services.AddScoped<AppApiFactory, HubAppApiFactory>();
        services.AddScoped<XTI_HubWebAppApiActions.Installations.InstallationQueryAction>();
        services.AddScoped<XTI_HubWebAppApiActions.Logs.AppRequestQueryAction>();
        services.AddScoped<XTI_HubWebAppApiActions.Logs.LogEntryQueryAction>();
        services.AddScoped<XTI_HubWebAppApiActions.Logs.SessionQueryAction>();
        services.AddScoped<XTI_HubWebAppApiActions.UserGroups.UserQueryAction>();
        services.AddScoped<XTI_HubWebAppApiActions.UserRoles.UserRoleQueryAction>();
        services.AddMoreServices();
    }

    static partial void AddMoreServices(this IServiceCollection services);
}