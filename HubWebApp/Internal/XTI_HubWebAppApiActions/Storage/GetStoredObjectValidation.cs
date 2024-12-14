using XTI_Core;

namespace XTI_HubWebAppApiActions.Storage;

public sealed class GetStoredObjectValidation : AppActionValidation<GetStoredObjectRequest>
{
    public Task Validate(ErrorList errors, GetStoredObjectRequest model, CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(model.StorageName))
        {
            errors.Add(StorageErrors.StorageNameIsRequired);
        }
        if (string.IsNullOrWhiteSpace(model.StorageKey))
        {
            errors.Add(StorageErrors.StorageKeyIsRequired);
        }
        return Task.CompletedTask;
    }
}
