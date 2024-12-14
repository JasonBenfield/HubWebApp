using XTI_HubWebAppApiActions.UserRoles;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class UserRolesApiGroupExtensions
{
    internal static void AddUserRolesServices(this IServiceCollection services)
    {
        services.AddScoped<DeleteUserRoleAction>();
        services.AddScoped<GetUserRoleDetailAction>();
        services.AddScoped<IndexPage>();
        services.AddScoped<UserRolePage>();
    }
}