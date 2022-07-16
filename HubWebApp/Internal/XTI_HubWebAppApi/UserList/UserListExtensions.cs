using XTI_HubWebAppApi.UserList;

namespace XTI_HubWebAppApi;

internal static class UserListExtensions
{
    public static void AddUserListGroupServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
        services.AddScoped<AddOrUpdateUserAction>();
        services.AddScoped<AddUserValidation>();
        services.AddScoped<GetUsersAction>();
    }
}