using XTI_HubWebAppApiActions.Installations;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class InstallationsApiGroupExtensions
{
    internal static void AddInstallationsServices(this IServiceCollection services)
    {
        services.AddScoped<BeginDeleteAction>();
        services.AddScoped<DeletedAction>();
        services.AddScoped<GetInstallationDetailAction>();
        services.AddScoped<GetPendingDeletesAction>();
        services.AddScoped<IndexAction>();
        services.AddScoped<InstallationPage>();
        services.AddScoped<RequestDeleteAction>();
    }
}