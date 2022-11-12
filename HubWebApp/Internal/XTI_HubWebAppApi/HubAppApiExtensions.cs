namespace XTI_HubWebAppApi;

public static class HubAppApiExtensions
{
    public static void AddHubAppApiServices(this IServiceCollection services)
    {
        services.AddScoped<CurrentAppUser>();
        services.AddScoped<UserGroupFromPath>();
        services.AddCurrentUserGroupServices();
        services.AddHomeGroupServices();
        services.AddAuthenticatorsGroupServices();
        services.AddAppInquiryGroupServices();
        services.AddAppInstallGroupServices();
        services.AddAppListGroupServices();
        services.AddAppPublishGroupServices();
        services.AddAppUserGroupServices();
        services.AddAppUserMaintenanceGroupServices();
        services.AddExternalAuthGroupServices();
        services.AddAuthGroupServices();
        services.AddModCategoryGroupExtensions();
        services.AddPermanentLogGroupServices();
        services.AddResourceGroupInquiryGroupServices();
        services.AddResourceInquiryGroupServices();
        services.AddUserInquiryGroupServices();
        services.AddUserListGroupServices();
        services.AddUserMaintenanceGroupServices();
        services.AddVersionInquiryGroupServices();
        services.AddStorageGroupServices();
        services.AddSystemGroupServices();
        services.AddUserGroupsGroupServices();
        services.AddPeriodicGroupServices();
        services.AddLogsGroupServices();
    }
}