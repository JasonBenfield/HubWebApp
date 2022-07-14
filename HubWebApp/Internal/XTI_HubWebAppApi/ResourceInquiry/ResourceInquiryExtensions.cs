using XTI_HubWebAppApi.ResourceInquiry;

namespace XTI_HubWebAppApi;

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