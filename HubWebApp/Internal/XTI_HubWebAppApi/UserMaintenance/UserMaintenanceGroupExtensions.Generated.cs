using XTI_HubWebAppApiActions.UserMaintenance;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class UserMaintenanceGroupExtensions
{
    internal static void AddUserMaintenanceServices(this IServiceCollection services)
    {
        services.AddScoped<ChangePasswordAction>();
        services.AddScoped<DeactivateUserAction>();
        services.AddScoped<EditUserAction>();
        services.AddScoped<GetUserForEditAction>();
        services.AddScoped<ReactivateUserAction>();
    }
}