namespace XTI_Hub.Abstractions;

public interface IStoredObjectDB
{
    Task<string> Store(StorageName storageName, GeneratedStorageKeyType generatedStorageKeyType, string data, TimeSpan expireAfter);

    Task<string> Value(StorageName storageName, string storageKey);
}
