using XTI_Core;

namespace XTI_HubWebAppApiActions.Storage;

public sealed class GetStoredObjectValidation : AppActionValidation<GetStoredObjectRequest>
{
    public Task Validate(ErrorList errors, GetStoredObjectRequest getRequest, CancellationToken stoppingToken)
    {
        getRequest.Validate(errors);
        return Task.CompletedTask;
    }
}
