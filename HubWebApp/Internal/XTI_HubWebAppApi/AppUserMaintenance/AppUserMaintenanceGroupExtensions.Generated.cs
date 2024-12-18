using XTI_HubWebAppApiActions.AppUserMaintenance;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class AppUserMaintenanceGroupExtensions
{
    internal static void AddAppUserMaintenanceServices(this IServiceCollection services)
    {
        services.AddScoped<AllowAccessAction>();
        services.AddScoped<AssignRoleAction>();
        services.AddScoped<DenyAccessAction>();
        services.AddScoped<UnassignRoleAction>();
    }
}