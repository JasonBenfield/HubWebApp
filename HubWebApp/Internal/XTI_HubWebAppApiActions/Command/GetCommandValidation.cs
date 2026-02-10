using XTI_Core;

namespace XTI_HubWebAppApiActions.Command;

public sealed class GetCommandValidation : AppActionValidation<AppCommandIDRequest>
{
    public Task Validate(ErrorList errors, AppCommandIDRequest requestData, CancellationToken stoppingToken)
    {
        if(requestData.CommandID <= 0)
        {
            errors.Add("Command ID is required.");
        }
        return Task.CompletedTask;
    }
}
