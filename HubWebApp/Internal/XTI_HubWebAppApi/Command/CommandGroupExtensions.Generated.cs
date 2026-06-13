using XTI_HubWebAppApiActions.Command;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class CommandGroupExtensions
{
    internal static void AddCommandServices(this IServiceCollection services)
    {
        services.AddScoped<BeginCommandAction>();
        services.AddScoped<BeginCommandValidation>();
        services.AddScoped<BeginCommandStepAction>();
        services.AddScoped<BeginCommandStepValidation>();
        services.AddScoped<BeginInstallationAction>();
        services.AddScoped<BeginInstallationValidation>();
        services.AddScoped<CommandEndedAction>();
        services.AddScoped<CommandEndedValidation>();
        services.AddScoped<CommandStepEndedAction>();
        services.AddScoped<CommandStepEndedValidation>();
        services.AddScoped<GetCommandAction>();
        services.AddScoped<GetCommandValidation>();
        services.AddScoped<GetDeleteCommandDetailAction>();
        services.AddScoped<GetDeleteCommandDetailValidation>();
        services.AddScoped<GetInstallationCommandDetailAction>();
        services.AddScoped<GetInstallationCommandDetailValidation>();
        services.AddScoped<IndexAction>();
    }
}