using XTI_HubWebAppApi.Installations;

namespace XTI_HubWebAppApi;

internal static class InstallationsGroupExtensions
{
    public static void AddInstallationsGroupServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
        services.AddScoped<InstallationQueryAction>();
        services.AddScoped<GetPendingDeletesAction>();
        services.AddScoped<RequestDeleteAction>();
        services.AddScoped<BeginDeleteAction>();
        services.AddScoped<DeletedAction>();
    }
}