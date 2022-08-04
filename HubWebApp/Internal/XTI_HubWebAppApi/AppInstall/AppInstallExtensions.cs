using XTI_HubWebAppApi.AppInstall;

namespace XTI_HubWebAppApi;

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
        services.AddScoped<BeginInstallationAction>();
        services.AddScoped<GetVersionAction>();
        services.AddScoped<GetVersionsAction>();
        services.AddScoped<InstalledAction>();
        services.AddScoped<NewInstallationValidation>();
        services.AddScoped<NewInstallationAction>();
        services.AddScoped<RegisterAppAction>();
        services.AddScoped<SetUserAccessAction>();
    }
}