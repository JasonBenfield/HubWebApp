using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.AppInquiry;

namespace XTI_HubAppApi;

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