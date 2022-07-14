namespace XTI_HubWebAppApi.Storage;

public sealed class StoredObjectNotFoundException : AppException
{
    public StoredObjectNotFoundException(string storageName, string storageKey) 
        : base($"Stored object not found named '{storageName}' with key '{storageKey}'", StorageErrors.StoredObjectNotFound)
    {
    }
}
