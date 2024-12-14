using XTI_HubWebAppApiActions.ModCategoryInquiry;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class ModCategoryApiGroupExtensions
{
    internal static void AddModCategoryServices(this IServiceCollection services)
    {
        services.AddScoped<GetModCategoryAction>();
        services.AddScoped<GetModifiersAction>();
        services.AddScoped<GetResourceGroupsAction>();
    }
}