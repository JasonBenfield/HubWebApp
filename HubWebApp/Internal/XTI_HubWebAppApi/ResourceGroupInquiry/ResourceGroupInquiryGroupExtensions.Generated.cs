using XTI_HubWebAppApiActions.ResourceGroupInquiry;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class ResourceGroupInquiryGroupExtensions
{
    internal static void AddResourceGroupInquiryServices(this IServiceCollection services)
    {
        services.AddScoped<GetModCategoryAction>();
        services.AddScoped<GetMostRecentErrorEventsAction>();
        services.AddScoped<GetMostRecentRequestsAction>();
        services.AddScoped<GetResourceGroupAction>();
        services.AddScoped<GetResourcesAction>();
        services.AddScoped<GetRoleAccessAction>();
    }
}