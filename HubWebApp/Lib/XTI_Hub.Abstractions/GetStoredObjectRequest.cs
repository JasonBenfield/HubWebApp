namespace XTI_Hub.Abstractions;

public sealed class GetStoredObjectRequest
{
    public GetStoredObjectRequest()
        : this(new StorageName(), "")
    {
    }

    public GetStoredObjectRequest(StorageName storageName, string storageKey)
    {
        StorageName = storageName.DisplayText;
        StorageKey = storageKey;
    }

    public string StorageName { get; set; }
    public string StorageKey { get; set; } 
}
