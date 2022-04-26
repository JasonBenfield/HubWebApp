using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.UserList;

namespace XTI_HubAppApi;

internal static class UserListExtensions
{
    public static void AddUserListGroupServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
        services.AddScoped<AddOrUpdateUserAction>();
        services.AddScoped<AddUserValidation>();
        services.AddScoped<GetSystemUsersAction>();
        services.AddScoped<GetUsersAction>();
    }
}