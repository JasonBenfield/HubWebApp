using XTI_HubWebAppApiActions.ResourceInquiry;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class ResourceInquiryApiGroupExtensions
{
    internal static void AddResourceInquiryServices(this IServiceCollection services)
    {
        services.AddScoped<GetMostRecentErrorEventsAction>();
        services.AddScoped<GetMostRecentRequestsAction>();
        services.AddScoped<GetResourceAction>();
        services.AddScoped<GetRoleAccessAction>();
    }
}