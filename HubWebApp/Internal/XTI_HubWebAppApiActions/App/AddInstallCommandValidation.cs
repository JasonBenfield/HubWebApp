using XTI_Core;

namespace XTI_HubWebAppApiActions.App;

public sealed class AddInstallCommandValidation : AppActionValidation<InstallConfigurationIDRequest>
{
    public Task Validate(ErrorList errors, InstallConfigurationIDRequest requestData, CancellationToken stoppingToken)
    {
        if (requestData.ConfigurationID <= 0)
        {
            errors.Add("Configuration ID is required.");
        }
        return Task.CompletedTask;
    }
}