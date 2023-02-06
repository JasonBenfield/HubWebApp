using XTI_HubWebAppApi.UserMaintenance;

namespace XTI_HubWebAppApi;

internal static class UserMaintenanceExtensions
{
    public static void AddUserMaintenanceGroupServices(this IServiceCollection services)
    {
        services.AddScoped<ChangePasswordAction>();
        services.AddScoped<DeactivateUserAction>();
        services.AddScoped<EditUserAction>();
        services.AddScoped<GetUserForEditAction>();
        services.AddScoped<ReactivateUserAction>();
    }
}