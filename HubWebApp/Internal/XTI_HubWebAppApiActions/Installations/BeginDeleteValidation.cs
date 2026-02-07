using XTI_Core;

namespace XTI_HubWebAppApiActions.Installations;

public sealed class BeginDeleteValidation : AppActionValidation<InstallationIDRequest>
{
    public Task Validate(ErrorList errors, InstallationIDRequest requestData, CancellationToken stoppingToken)
    {
        if (requestData.InstallationID <= 0)
        {
            errors.Add("Installation ID is required.");
        }
        return Task.CompletedTask;
    }
}