using XTI_Core;

namespace XTI_HubWebAppApiActions.Installations;

public sealed class BeginInstallationValidation : AppActionValidation<BeginInstallationRequest>
{
    public Task Validate(ErrorList errors, BeginInstallationRequest requestData, CancellationToken stoppingToken)
    {
        if(requestData.CommandID <= 0)
        {
            errors.Add("Requested Installation ID is required.");
        }
        return Task.CompletedTask;
    }
}