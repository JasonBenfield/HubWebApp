using XTI_HubWebAppApiActions.Installations;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class InstallationsGroupExtensions
{
    internal static void AddInstallationsServices(this IServiceCollection services)
    {
        services.AddScoped<BeginDeleteAction>();
        services.AddScoped<BeginDeleteValidation>();
        services.AddScoped<BeginInstallationAction>();
        services.AddScoped<BeginInstallationValidation>();
        services.AddScoped<BeginRequestedInstallationAction>();
        services.AddScoped<BeginRequestedInstallationValidation>();
        services.AddScoped<BeginRequestedInstallationStepAction>();
        services.AddScoped<DeletedAction>();
        services.AddScoped<DeletedValidation>();
        services.AddScoped<GetInstallationActivitiesAction>();
        services.AddScoped<GetInstallationDetailAction>();
        services.AddScoped<GetInstallationDetailValidation>();
        services.AddScoped<GetRequestedInstallationDetailAction>();
        services.AddScoped<GetRequestedInstallationDetailValidation>();
        services.AddScoped<IndexAction>();
        services.AddScoped<InstallationPage>();
        services.AddScoped<InstalledAction>();
        services.AddScoped<InstalledValidation>();
        services.AddScoped<RequestDeleteAction>();
        services.AddScoped<RequestDeleteValidation>();
        services.AddScoped<RequestedInstallationEndedAction>();
        services.AddScoped<RequestedInstallationEndedValidation>();
        services.AddScoped<RequestedInstallationStepEndedAction>();
        services.AddScoped<RequestedInstallationStepEndedValidation>();
        services.AddScoped<RequestInstallationAction>();
        services.AddScoped<RequestInstallationValidation>();
    }
}