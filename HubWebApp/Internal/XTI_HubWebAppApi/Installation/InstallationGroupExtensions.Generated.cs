using XTI_HubWebAppApiActions.Installation;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class InstallationGroupExtensions
{
    internal static void AddInstallationServices(this IServiceCollection services)
    {
        services.AddScoped<AddDeleteCommandAction>();
        services.AddScoped<AddDeleteCommandValidation>();
        services.AddScoped<BeginDeleteAction>();
        services.AddScoped<BeginDeleteValidation>();
        services.AddScoped<DeletedAction>();
        services.AddScoped<DeletedValidation>();
        services.AddScoped<GetInstallationDetailAction>();
        services.AddScoped<GetInstallationDetailValidation>();
        services.AddScoped<IndexPage>();
        services.AddScoped<InstalledAction>();
        services.AddScoped<InstalledValidation>();
    }
}