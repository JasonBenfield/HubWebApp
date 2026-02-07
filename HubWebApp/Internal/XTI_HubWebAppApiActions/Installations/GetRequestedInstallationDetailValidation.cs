using XTI_Core;

namespace XTI_HubWebAppApiActions.Installations;

public sealed class GetRequestedInstallationDetailValidation : AppActionValidation<AppCommandIDRequest>
{
    public Task Validate(ErrorList errors, AppCommandIDRequest requestData, CancellationToken stoppingToken)
    {
        if (requestData.CommandID <= 0)
        {
            errors.Add("Requested Installation ID is required.");
        }
        return Task.CompletedTask;
    }
}
