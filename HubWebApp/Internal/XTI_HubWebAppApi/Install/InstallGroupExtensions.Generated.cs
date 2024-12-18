using XTI_HubWebAppApiActions.AppInstall;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class InstallGroupExtensions
{
    internal static void AddInstallServices(this IServiceCollection services)
    {
        services.AddScoped<AddAdminUserAction>();
        services.AddScoped<AddInstallationUserAction>();
        services.AddScoped<AddOrUpdateAppsAction>();
        services.AddScoped<AddOrUpdateAppsValidation>();
        services.AddScoped<AddOrUpdateVersionsAction>();
        services.AddScoped<AddSystemUserAction>();
        services.AddScoped<AddSystemUserValidation>();
        services.AddScoped<BeginInstallationAction>();
        services.AddScoped<ConfigureInstallAction>();
        services.AddScoped<ConfigureInstallValidation>();
        services.AddScoped<ConfigureInstallTemplateAction>();
        services.AddScoped<ConfigureInstallTemplateValidation>();
        services.AddScoped<DeleteInstallConfigurationAction>();
        services.AddScoped<DeleteInstallConfigurationValidation>();
        services.AddScoped<GetInstallConfigurationsAction>();
        services.AddScoped<GetInstallConfigurationsValidation>();
        services.AddScoped<GetVersionAction>();
        services.AddScoped<GetVersionsAction>();
        services.AddScoped<InstalledAction>();
        services.AddScoped<NewInstallationAction>();
        services.AddScoped<NewInstallationValidation>();
        services.AddScoped<RegisterAppAction>();
        services.AddScoped<SetUserAccessAction>();
    }
}