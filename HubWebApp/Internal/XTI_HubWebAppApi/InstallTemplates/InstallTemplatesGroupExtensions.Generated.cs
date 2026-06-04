using XTI_HubWebAppApiActions.InstallTemplates;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class InstallTemplatesGroupExtensions
{
    internal static void AddInstallTemplatesServices(this IServiceCollection services)
    {
        services.AddScoped<GetInstallTemplatesAction>();
        services.AddScoped<IndexAction>();
    }
}