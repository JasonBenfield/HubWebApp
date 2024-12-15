using XTI_SupportServiceAppApi.Installations;

// Generated Code
namespace XTI_SupportServiceAppApi;
internal static partial class InstallationsApiGroupExtensions
{
    internal static void AddInstallationsServices(this IServiceCollection services)
    {
        services.AddScoped<DeleteAction>();
    }
}