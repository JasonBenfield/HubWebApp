using XTI_HubWebAppApi.AppUserMaintenance;

namespace XTI_HubWebAppApi;

internal static class AppUserMaintenanceExtensions
{
    public static void AddAppUserMaintenanceGroupServices(this IServiceCollection services)
    {
        services.AddScoped<AssignRoleAction>();
        services.AddScoped<UnassignRoleAction>();
        services.AddScoped<DenyAccessAction>();
        services.AddScoped<AllowAccessAction>();
    }
}