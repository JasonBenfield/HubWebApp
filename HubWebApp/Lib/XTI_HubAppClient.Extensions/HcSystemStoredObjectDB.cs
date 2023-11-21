using XTI_Hub.Abstractions;

namespace XTI_HubAppClient.Extensions;

public sealed class HcSystemStoredObjectDB : IStoredObjectDB
{
    private readonly HubAppClient hubClient;

    public HcSystemStoredObjectDB(HubAppClient hubClient)
    {
        this.hubClient = hubClient;
    }

    public Task<string> Store(StorageName storageName, GenerateKeyModel generateKey, string data, TimeSpan expiresAfter, bool isSingleUse)
    {
        hubClient.UseToken<SystemUserXtiToken>();
        return hubClient.System.StoreObject
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
    }

    public Task<string> Value(StorageName storageName, string storageKey)
    {
        hubClient.UseToken<SystemUserXtiToken>();
        return hubClient.System.GetStoredObject
        (
            new GetStoredObjectRequest
            {
                StorageName = storageName.Value,
                StorageKey = storageKey
            }
        );
    }
}
