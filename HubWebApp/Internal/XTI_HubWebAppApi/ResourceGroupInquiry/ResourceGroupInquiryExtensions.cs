using XTI_HubWebAppApi.ResourceGroupInquiry;

namespace XTI_HubWebAppApi;

internal static class ResourceGroupInquiryExtensions
{
    public static void AddResourceGroupInquiryGroupServices(this IServiceCollection services)
    {
        services.AddScoped<GetModCategoryAction>();
        services.AddScoped<GetMostRecentErrorEventsAction>();
        services.AddScoped<GetMostRecentRequestsAction>();
        services.AddScoped<GetResourceGroupAction>();
        services.AddScoped<GetResourcesAction>();
        services.AddScoped<GetRoleAccessAction>();
    }
}