using XTI_SupportServiceAppApi.Installations;

namespace XTI_SupportServiceAppApi;

internal static class InstallationsGroupExtensions
{
    public static void AddInstallationsGroupServices(this IServiceCollection services)
    {
        services.AddScoped<DeleteAction>();
    }
}