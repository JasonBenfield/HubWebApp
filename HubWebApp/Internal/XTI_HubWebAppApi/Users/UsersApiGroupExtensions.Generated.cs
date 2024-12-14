using XTI_HubWebAppApiActions.UserList;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class UsersApiGroupExtensions
{
    internal static void AddUsersServices(this IServiceCollection services)
    {
        services.AddScoped<AddOrUpdateUserAction>();
        services.AddScoped<AddOrUpdateUserValidation>();
        services.AddScoped<AddUserAction>();
        services.AddScoped<GetUserGroupAction>();
        services.AddScoped<GetUsersAction>();
        services.AddScoped<IndexAction>();
    }
}