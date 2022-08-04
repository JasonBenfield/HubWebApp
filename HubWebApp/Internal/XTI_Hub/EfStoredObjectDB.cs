using XTI_Core;
using XTI_Hub.Abstractions;

namespace XTI_Hub;

public sealed class EfStoredObjectDB : IStoredObjectDB
{
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public EfStoredObjectDB(HubFactory hubFactory, IClock clock)
    {
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public Task<string> Store(StorageName storageName, GenerateKeyModel generatedKey, string data, TimeSpan expiresAfter)
    {
        var generatedStorageKey = new GeneratedKeyFactory().Create(generatedKey);
        return hubFactory.StoredObjects.AddOrUpdate
        (
            storageName,
            generatedStorageKey,
            data,
            clock.Now().Add(expiresAfter)
        );
    }

    public Task<string> Value(StorageName storageName, string storageKey) =>
        hubFactory.StoredObjects.StoredObject(storageName, storageKey, clock.Now());
}
