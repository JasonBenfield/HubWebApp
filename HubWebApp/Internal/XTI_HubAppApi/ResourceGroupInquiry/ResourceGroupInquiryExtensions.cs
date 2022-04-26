using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.ResourceGroupInquiry;

namespace XTI_HubAppApi;

internal static class ResourceGroupInquiryExtensions
{
    public static void AddResourceGroupInquiryGroupServices(this IServiceCollection services)
    {
        services.AddScoped<GetModCategoryAction>();
        services.AddScoped<GetMostRecentErrorEventsAction>();
        services.AddScoped<GetMostRecentRequestsAction>();
        services.AddScoped<GetResourceAction>();
        services.AddScoped<GetResourceGroupAction>();
        services.AddScoped<GetResourcesAction>();
        services.AddScoped<GetRoleAccessAction>();
    }
}