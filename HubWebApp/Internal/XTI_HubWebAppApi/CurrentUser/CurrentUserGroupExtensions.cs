using XTI_HubWebAppApi.CurrentUser;

namespace XTI_HubWebAppApi;

internal static class CurrentUserGroupExtensions
{
    public static void AddCurrentUserGroupServices(this IServiceCollection services)
    {
        services.AddScoped<GetUserAction>();
        services.AddScoped<IndexAction>();
    }
}