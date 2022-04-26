using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.AppInstall;

namespace XTI_HubAppApi;

internal static class AppInstallExtensions
{
    public static void AddAppInstallGroupServices(this IServiceCollection services)
    {
        services.AddScoped<AddOrUpdateAppsAction>();
        services.AddScoped<AddOrUpdateAppsValidation>();
        services.AddScoped<AddOrUpdateVersionsAction>();
        services.AddScoped<AddSystemUserValidation>();
        services.AddScoped<AddInstallationUserAction>();
        services.AddScoped<AddSystemUserAction>();
        services.AddScoped<BeginCurrentInstallationAction>();
        services.AddScoped<BeginVersionInstallationAction>();
        services.AddScoped<GetVersionAction>();
        services.AddScoped<GetVersionsAction>();
        services.AddScoped<InstalledAction>();
        services.AddScoped<NewInstallationValidation>();
        services.AddScoped<NewInstallationAction>();
        services.AddScoped<RegisterAppAction>();
    }
}