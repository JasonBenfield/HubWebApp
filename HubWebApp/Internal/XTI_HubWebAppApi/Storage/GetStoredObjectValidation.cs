using XTI_Core;

namespace XTI_HubWebAppApi.Storage;

internal sealed class GetStoredObjectValidation : AppActionValidation<GetStoredObjectRequest>
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
