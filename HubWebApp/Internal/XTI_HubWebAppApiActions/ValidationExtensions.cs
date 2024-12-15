using XTI_Core;
using XTI_HubWebAppApiActions.Storage;

namespace XTI_HubWebAppApiActions;

internal static class ValidationExtensions
{
    internal static void Validate(this StoreObjectRequest storeRequest, ErrorList errors)
    {
        if (string.IsNullOrWhiteSpace(storeRequest.StorageName))
        {
            errors.Add(StorageErrors.StorageNameIsRequired);
        }
        if (string.IsNullOrWhiteSpace(storeRequest.Data))
        {
            errors.Add(StorageErrors.StorageDataIsRequired);
        }
    }

    internal static void Validate(this GetStoredObjectRequest getRequest, ErrorList errors)
    {
        if (string.IsNullOrWhiteSpace(getRequest.StorageName))
        {
            errors.Add(StorageErrors.StorageNameIsRequired);
        }
        if (string.IsNullOrWhiteSpace(getRequest.StorageKey))
        {
            errors.Add(StorageErrors.StorageKeyIsRequired);
        }
    }
}
