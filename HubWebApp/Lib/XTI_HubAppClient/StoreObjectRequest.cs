// Generated Code
namespace XTI_HubAppClient;
public sealed partial class StoreObjectRequest
{
    public string StorageName { get; set; } = "";
    public string Data { get; set; } = "";
    public TimeSpan ExpireAfter { get; set; }

    public GenerateKeyModel GenerateKey { get; set; } = new GenerateKeyModel();
}