using XTI_HubAppApi.AppUserMaintenance;

namespace XTI_HubAppApi;

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