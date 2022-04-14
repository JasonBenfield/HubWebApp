using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.UserMaintenance;

namespace XTI_HubAppApi;

internal static class UserMaintenanceExtensions
{
    public static void AddUserMaintenanceGroupServices(this IServiceCollection services)
    {
        services.AddScoped<EditUserAction>();
        services.AddScoped<GetUserForEditAction>();
    }
}