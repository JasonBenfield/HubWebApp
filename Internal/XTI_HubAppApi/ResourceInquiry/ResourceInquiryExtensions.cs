using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.ResourceInquiry;

namespace XTI_HubAppApi;

internal static class ResourceInquiryExtensions
{
    public static void AddResourceInquiryGroupServices(this IServiceCollection services)
    {
        services.AddScoped<GetMostRecentErrorEventsAction>();
        services.AddScoped<GetMostRecentRequestsAction>();
        services.AddScoped<GetResourceAction>();
        services.AddScoped<GetRoleAccessAction>();
    }
}