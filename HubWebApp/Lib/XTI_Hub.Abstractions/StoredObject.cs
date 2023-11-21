using XTI_Core;

namespace XTI_Hub.Abstractions;

public sealed class StoredObject
{
    private readonly IStoredObjectDB db;
    private readonly StorageName storageName;

    public StoredObject(IStoredObjectDB db, StorageName storageName)
    {
        this.db = db;
        this.storageName = storageName;
    }

    public Task<string> Store(GenerateKeyModel generateKeyModel, object data, TimeSpan expireAfter) =>
        Store(generateKeyModel, data, expireAfter, false);

    public Task<string> StoreSingleUse(GenerateKeyModel generateKeyModel, object data, TimeSpan expireAfter) =>
        Store(generateKeyModel, data, expireAfter, true);

    private Task<string> Store(GenerateKeyModel generateKeyModel, object data, TimeSpan expireAfter, bool isSingleUse)
    {
        var serialized = data is string str ? str : XtiSerializer.Serialize(data);
        return db.Store(storageName, generateKeyModel, serialized, expireAfter, isSingleUse);
    }

    public Task<T> Value<T>(string storageKey) 
        where T : new() => 
            Value(storageKey, () => new T());

    public async Task<T> Value<T>(string storageKey, Func<T> ifnull)
    {
        var data = await SerializedValue(storageKey);
        return string.IsNullOrWhiteSpace(data) ? ifnull() : XtiSerializer.Deserialize(data, ifnull);
    }

    public Task<string> SerializedValue(string storageKey) => db.Value(storageName, storageKey);

}
