// Generated Code
namespace XTI_HubAppClient;
public sealed partial class StoreObjectRequest
{
    public string StorageName { get; set; } = "";
    public string Data { get; set; } = "";
    public DateTimeOffset TimeExpires { get; set; }

    public GeneratedStorageKeyType GeneratedStorageKeyType { get; set; } = GeneratedStorageKeyType.Values.GetDefault();
}