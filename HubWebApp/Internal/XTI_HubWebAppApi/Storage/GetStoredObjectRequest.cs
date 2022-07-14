namespace XTI_HubWebAppApi.Storage;

public sealed class GetStoredObjectRequest
{
    public string StorageName { get; set; } = "";
    public string StorageKey { get; set; } = "";
}
