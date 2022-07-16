namespace XTI_Hub.Abstractions;

public interface IStoredObjectDB
{
    Task<string> Store(StorageName storageName, GenerateKeyModel generateKey, string data, TimeSpan expireAfter);

    Task<string> Value(StorageName storageName, string storageKey);
}
