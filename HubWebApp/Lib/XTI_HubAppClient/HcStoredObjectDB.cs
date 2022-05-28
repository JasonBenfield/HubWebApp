namespace XTI_HubAppClient;

public sealed class HcStoredObjectDB : IStoredObjectDB
{
    private readonly HubAppClient hubClient;

    public HcStoredObjectDB(HubAppClient hubClient)
    {
        this.hubClient = hubClient;
    }

    public Task<string> Store(StorageName storageName, GeneratedStorageKeyType generatedStorageKeyType, string data, TimeSpan expiresAfter) =>
        hubClient.Storage.StoreObject
        (
            new StoreObjectRequest
            {
                StorageName = storageName.Value,
                GeneratedStorageKeyType = generatedStorageKeyType,
                Data = data,
                ExpireAfter = expiresAfter
            }
        );

    public Task<string> Value(StorageName storageName, string storageKey) =>
        hubClient.Storage.GetStoredObject
        (
            new GetStoredObjectRequest
            {
                StorageName = storageName.Value,
                StorageKey = storageKey
            }
        );
}
