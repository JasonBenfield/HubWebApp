using XTI_HubWebAppApi.UserRoles;

namespace XTI_HubWebAppApi;

internal static class UserRolesGroupExtensions
{
    public static void AddUserRolesGroupServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
        services.AddScoped<GetUserRoleDetailAction>();
    }
}