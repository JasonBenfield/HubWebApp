namespace XTI_HubAppApi.Storage;

public sealed class StoreObjectRequest
{
    public string StorageName { get; set; } = "";
    public string Data { get; set; } = "";
    public TimeSpan ExpireAfter { get; set; } = TimeSpan.Zero;
    public GeneratedStorageKeyType GeneratedStorageKeyType { get; set; } = GeneratedStorageKeyType.Values.GetDefault();
}
