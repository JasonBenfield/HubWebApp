using XTI_HubAppApi.ModCategoryInquiry;

namespace XTI_HubAppApi;

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