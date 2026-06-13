using XTI_Core;

namespace XTI_HubWebAppApiActions.Command;

public sealed class BeginCommandStepValidation : AppActionValidation<BeginAppCommandStepRequest>
{
    public Task Validate(ErrorList errors, BeginAppCommandStepRequest requestData, CancellationToken stoppingToken)
    {
        if(requestData.CommandID <= 0)
        {
            errors.Add("Command ID is required.");
        }
        if (string.IsNullOrWhiteSpace(requestData.Activity))
        {
            errors.Add("Activity is required.");
        }
        return Task.CompletedTask;
    }
}
