using XTI_SupportServiceAppApi.Installations;

// Generated Code
namespace XTI_SupportServiceAppApi;
internal static partial class InstallationsGroupExtensions
{
    internal static void AddInstallationsServices(this IServiceCollection services)
    {
        services.AddScoped<DeleteAction>();
    }
}