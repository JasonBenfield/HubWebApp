namespace XTI_Hub.Abstractions;

public sealed class StoreObjectRequest
{
    public StoreObjectRequest()
        : this(new StorageName(), "", TimeSpan.Zero, GenerateKeyModel.Guid())
    {
    }

    public StoreObjectRequest(StorageName storageName, string data, TimeSpan expireAfter, GenerateKeyModel generateKey)
    {
        StorageName = storageName.DisplayText;
        Data = data;
        ExpireAfter = expireAfter;
        GenerateKey = generateKey;
    }

    public string StorageName { get; set; }
    public string Data { get; set; }
    public TimeSpan ExpireAfter { get; set; }
    public GenerateKeyModel GenerateKey { get; set; }
}
