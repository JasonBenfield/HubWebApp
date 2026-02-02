using XTI_HubWebAppApiActions.Installations;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class InstallationsGroupExtensions
{
    internal static void AddInstallationsServices(this IServiceCollection services)
    {
        services.AddScoped<BeginDeleteAction>();
        services.AddScoped<DeletedAction>();
        services.AddScoped<GetInstallationActivitiesAction>();
        services.AddScoped<GetInstallationDetailAction>();
        services.AddScoped<IndexAction>();
        services.AddScoped<InstallationPage>();
        services.AddScoped<RequestDeleteAction>();
    }
}