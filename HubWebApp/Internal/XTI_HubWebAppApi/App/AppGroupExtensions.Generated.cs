using XTI_HubWebAppApiActions.AppInquiry;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class AppGroupExtensions
{
    internal static void AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<GetAppAction>();
        services.AddScoped<GetDefaultAppOptionsAction>();
        services.AddScoped<GetDefaultModifierAction>();
        services.AddScoped<GetDefaultOptionsAction>();
        services.AddScoped<GetModifierCategoriesAction>();
        services.AddScoped<GetMostRecentErrorEventsAction>();
        services.AddScoped<GetMostRecentRequestsAction>();
        services.AddScoped<GetResourceGroupsAction>();
        services.AddScoped<GetRolesAction>();
        services.AddScoped<IndexAction>();
    }
}