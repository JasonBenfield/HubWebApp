using XTI_Core;

namespace XTI_HubWebAppApiActions.Storage;

public sealed class StoreObjectAction : AppAction<StoreObjectRequest, string>
{
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public StoreObjectAction(HubFactory hubFactory, IClock clock)
    {
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public async Task<string> Execute(StoreObjectRequest storeRequest, CancellationToken stoppingToken)
    {
        var storageName = new StorageName(storeRequest.StorageName);
        string storageKey;
        if (storeRequest.IsSingleUse)
        {
            storageKey = await hubFactory.StoredObjects.StoreSingleUse
            (
                storageName,
                storeRequest.GenerateKey,
                storeRequest.Data,
                clock,
                storeRequest.ExpireAfter
            );
        }
        else
        {
            storageKey = await hubFactory.StoredObjects.Store
            (
                storageName,
                storeRequest.GenerateKey,
                storeRequest.Data,
                clock,
                storeRequest.ExpireAfter,
                storeRequest.IsSlidingExpiration
            );
        }
        return storageKey;
    }
}
