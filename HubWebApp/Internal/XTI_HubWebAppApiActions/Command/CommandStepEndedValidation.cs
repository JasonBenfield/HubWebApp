using XTI_Core;

namespace XTI_HubWebAppApiActions.Command;

public sealed class CommandStepEndedValidation : AppActionValidation<AppCommandStepEndedRequest>
{
    public Task Validate(ErrorList errors, AppCommandStepEndedRequest requestData, CancellationToken stoppingToken)
    {
        if(requestData.StepID <= 0)
        {
            errors.Add("Step ID is required.");
        }
        return Task.CompletedTask;
    }
}
