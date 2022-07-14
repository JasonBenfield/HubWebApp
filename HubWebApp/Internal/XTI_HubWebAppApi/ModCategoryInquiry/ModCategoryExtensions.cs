using XTI_HubWebAppApi.ModCategoryInquiry;

namespace XTI_HubWebAppApi;

internal static class ModCategoryExtensions
{
    public static void AddModCategoryGroupExtensions(this IServiceCollection services)
    {
        services.AddScoped<GetModCategoryAction>();
        services.AddScoped<GetModifierAction>();
        services.AddScoped<GetModifiersAction>();
        services.AddScoped<GetResourceGroupsAction>();
    }
}