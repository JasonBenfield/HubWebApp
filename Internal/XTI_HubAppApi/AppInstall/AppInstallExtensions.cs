using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.AppInstall;

namespace XTI_HubAppApi;

internal static class AppInstallExtensions
{
    public static void AddAppInstallGroupServices(this IServiceCollection services)
    {
        services.AddScoped<AddSystemUserAction>();
        services.AddScoped<BeginCurrentInstallationAction>();
        services.AddScoped<BeginVersionInstallationAction>();
        services.AddScoped<GetVersionAction>();
        services.AddScoped<InstalledAction>();
        services.AddScoped<NewInstallationAction>();
        services.AddScoped<RegisterAppAction>();
    }
}