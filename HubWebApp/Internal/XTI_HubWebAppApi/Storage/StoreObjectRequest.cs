namespace XTI_HubWebAppApi.Storage;

public sealed class StoreObjectRequest
{
    public string StorageName { get; set; } = "";
    public string Data { get; set; } = "";
    public TimeSpan ExpireAfter { get; set; } = TimeSpan.Zero;
    public GenerateKeyModel GenerateKey { get; set; } = GenerateKeyModel.Guid();
}
