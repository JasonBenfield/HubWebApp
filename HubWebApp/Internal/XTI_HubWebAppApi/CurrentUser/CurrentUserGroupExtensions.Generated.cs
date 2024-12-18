using XTI_HubWebAppApiActions.CurrentUser;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class CurrentUserGroupExtensions
{
    internal static void AddCurrentUserServices(this IServiceCollection services)
    {
        services.AddScoped<ChangePasswordAction>();
        services.AddScoped<EditUserAction>();
        services.AddScoped<GetUserAction>();
        services.AddScoped<IndexAction>();
    }
}