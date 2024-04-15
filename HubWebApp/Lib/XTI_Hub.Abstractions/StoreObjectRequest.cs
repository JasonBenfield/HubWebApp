namespace XTI_Hub.Abstractions;

public sealed class StoreObjectRequest
{
    public StoreObjectRequest()
        : this(new StorageName(), "", TimeSpan.Zero, GenerateKeyModel.Guid())
    {
    }

    public StoreObjectRequest(StorageName storageName, string data, TimeSpan expireAfter)
        : this(storageName, data, expireAfter, GenerateKeyModel.SixDigit())
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
    public bool IsSingleUse { get; set; }
    public bool IsSlidingExpiration { get; set; }

    public StoreObjectRequest SingleUse()
    {
        IsSingleUse = true;
        return this;
    }

    public StoreObjectRequest SlidingExpiration()
    {
        IsSlidingExpiration = true;
        return this;
    }
}
