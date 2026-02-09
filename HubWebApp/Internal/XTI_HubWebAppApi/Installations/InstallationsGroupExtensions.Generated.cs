using XTI_HubWebAppApiActions.Installations;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class InstallationsGroupExtensions
{
    internal static void AddInstallationsServices(this IServiceCollection services)
    {
        services.AddScoped<AddDeleteCommandAction>();
        services.AddScoped<AddDeleteCommandValidation>();
        services.AddScoped<AddInstallCommandAction>();
        services.AddScoped<AddInstallCommandValidation>();
        services.AddScoped<BeginCommandAction>();
        services.AddScoped<BeginCommandValidation>();
        services.AddScoped<BeginCommandStepAction>();
        services.AddScoped<BeginCommandStepValidation>();
        services.AddScoped<BeginDeleteAction>();
        services.AddScoped<BeginDeleteValidation>();
        services.AddScoped<BeginInstallationAction>();
        services.AddScoped<BeginInstallationValidation>();
        services.AddScoped<CommandEndedAction>();
        services.AddScoped<CommandEndedValidation>();
        services.AddScoped<CommandStepEndedAction>();
        services.AddScoped<CommandStepEndedValidation>();
        services.AddScoped<DeletedAction>();
        services.AddScoped<DeletedValidation>();
        services.AddScoped<GetDeleteCommandDetailAction>();
        services.AddScoped<GetDeleteCommandDetailValidation>();
        services.AddScoped<GetInstallationCommandDetailAction>();
        services.AddScoped<GetInstallationCommandDetailValidation>();
        services.AddScoped<GetInstallationDetailAction>();
        services.AddScoped<GetInstallationDetailValidation>();
        services.AddScoped<GetPendingCommandsAction>();
        services.AddScoped<IndexAction>();
        services.AddScoped<InstallationPage>();
        services.AddScoped<InstalledAction>();
        services.AddScoped<InstalledValidation>();
    }
}