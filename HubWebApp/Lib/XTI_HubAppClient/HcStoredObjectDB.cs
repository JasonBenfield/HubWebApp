namespace XTI_HubAppClient;

public sealed class HcStoredObjectDB : IStoredObjectDB
{
    private readonly HubAppClient hubClient;

    public HcStoredObjectDB(HubAppClient hubClient)
    {
        this.hubClient = hubClient;
    }

    public Task<string> Store(StorageName storageName, GenerateKeyModel generateKey, string data, TimeSpan expiresAfter, bool isSingleUse) =>
        hubClient.Storage.StoreObject
        (
            new StoreObjectRequest
            {
                StorageName = storageName.Value,
                GenerateKey = generateKey,
                Data = data,
                ExpireAfter = expiresAfter,
                IsSingleUse = isSingleUse
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
