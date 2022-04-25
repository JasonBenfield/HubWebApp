using XTI_SupportServiceAppApi.Home;

namespace XTI_SupportServiceAppApi;

internal static class HomeGroupExtensions
{
    public static void AddHomeGroupServices(this IServiceCollection services)
    {
        services.AddScoped<DoSomethingAction>();
    }
}