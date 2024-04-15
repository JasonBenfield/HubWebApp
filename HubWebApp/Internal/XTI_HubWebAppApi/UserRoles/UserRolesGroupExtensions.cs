using XTI_HubWebAppApi.UserRoles;

namespace XTI_HubWebAppApi;

internal static class UserRolesGroupExtensions
{
    public static void AddUserRolesGroupServices(this IServiceCollection services)
    {
        services.AddScoped<DeleteUserRoleAction>();
        services.AddScoped<IndexPage>();
        services.AddScoped<GetUserRoleDetailAction>();
        services.AddScoped<UserRoleQueryAction>();
        services.AddScoped<UserRolePage>();
    }
}