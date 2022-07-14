using XTI_HubWebAppApi.AppInquiry;

namespace XTI_HubWebAppApi;

internal static class AppInquiryExtensions
{
    public static void AddAppInquiryGroupServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
        services.AddScoped<GetAppAction>();
        services.AddScoped<GetDefaultModifierAction>();
        services.AddScoped<GetModifierCategoriesAction>();
        services.AddScoped<GetModifierCategoryAction>();
        services.AddScoped<GetMostRecentErrorEventsAction>();
        services.AddScoped<GetMostRecentRequestsAction>();
        services.AddScoped<GetResourceGroupsAction>();
        services.AddScoped<GetRoleAction>();
        services.AddScoped<GetRolesAction>();
    }
}