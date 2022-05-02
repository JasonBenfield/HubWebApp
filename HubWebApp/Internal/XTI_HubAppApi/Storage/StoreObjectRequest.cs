namespace XTI_HubAppApi.Storage;

public sealed class StoreObjectRequest
{
    public string StorageName { get; set; } = "";
    public string Data { get; set; } = "";
    public DateTimeOffset TimeExpires { get; set; } = DateTimeOffset.MaxValue;
    public GeneratedStorageKeyType GeneratedStorageKeyType { get; set; } = GeneratedStorageKeyType.Values.GetDefault();
}
