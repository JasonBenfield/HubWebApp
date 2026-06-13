using XTI_HubWebAppApiActions.Installations;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class InstallationsGroupExtensions
{
    internal static void AddInstallationsServices(this IServiceCollection services)
    {
        services.AddScoped<AddInstallCommandAction>();
        services.AddScoped<AddInstallCommandValidation>();
        services.AddScoped<IndexPage>();
    }
}