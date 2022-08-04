using XTI_HubWebAppApi.UserList;

namespace XTI_HubWebAppApi;

internal static class UserListExtensions
{
    public static void AddUserListGroupServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
        services.AddScoped<GetUserGroupAction>();
        services.AddScoped<AddOrUpdateUserAction>();
        services.AddScoped<AddOrUpdateUserValidation>();
        services.AddScoped<GetUsersAction>();
        services.AddScoped<AddUserAction>();
    }
}