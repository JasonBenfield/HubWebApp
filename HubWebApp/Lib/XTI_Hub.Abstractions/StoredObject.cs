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

    public Task<string> Store(GeneratedStorageKeyType generatedStorageKeyType, object data, TimeSpan expireAfter)
    {
        var serialized = data is string str ? str : XtiSerializer.Serialize(data);
        return db.Store(storageName, generatedStorageKeyType, serialized, expireAfter);
    }

    public Task<T> Value<T>(string storageKey) 
        where T : new() => 
            Value(storageKey, () => new T());

    public async Task<T> Value<T>(string storageKey, Func<T> ifnull)
    {
        var data = await SerializedValue(storageKey);
        return XtiSerializer.Deserialize(data, ifnull);
    }

    public Task<string> SerializedValue(string storageKey) => db.Value(storageName, storageKey);

}
