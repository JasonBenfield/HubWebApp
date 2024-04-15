namespace XTI_HubDB.Entities;

public sealed class StoredObjectEntity
{
    public int ID { get; set; }
    public string StorageName { get; set; } = "";
    public string StorageKey { get; set; } = "";
    public string Data { get; set; } = "";
    public DateTimeOffset TimeExpires { get; set; } = DateTimeOffset.MaxValue;
    public bool IsSingleUse { get; set; }
    public string ExpirationTimeSpan { get; set; } = "";
}
