namespace XTI_HubWebAppApi;

public static class HubAppApiExtensions
{
    public static void AddHubAppApiServices(this IServiceCollection services)
    {
        services.AddScoped<CurrentAppUser>();
        services.AddScoped<UserGroupFromPath>();
        services.AddAppInquiryGroupServices();
        services.AddAppInstallGroupServices();
        services.AddAppListGroupServices();
        services.AddAppPublishGroupServices();
        services.AddAppUserGroupServices();
        services.AddAppUserMaintenanceGroupServices();
        services.AddAuthenticatorsGroupServices();
        services.AddAuthGroupServices();
        services.AddCurrentUserGroupServices();
        services.AddExternalAuthGroupServices();
        services.AddHomeGroupServices();
        services.AddInstallationsGroupServices();
        services.AddLogsGroupServices();
        services.AddModCategoryGroupExtensions();
        services.AddPermanentLogGroupServices();
        services.AddResourceGroupInquiryGroupServices();
        services.AddResourceInquiryGroupServices();
        services.AddPeriodicGroupServices();
        services.AddStorageGroupServices();
        services.AddSystemGroupServices();
        services.AddUserGroupsGroupServices();
        services.AddUserInquiryGroupServices();
        services.AddUserListGroupServices();
        services.AddUserMaintenanceGroupServices();
        services.AddUserRolesGroupServices();
        services.AddVersionInquiryGroupServices();
    }
}