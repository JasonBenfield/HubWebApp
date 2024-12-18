using XTI_Core;

namespace XTI_HubWebAppApiActions.System;

public sealed class StoreObjectAction : AppAction<StoreObjectRequest, string>
{
    private readonly ICurrentUserName currentUserName;
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public StoreObjectAction(ICurrentUserName currentUserName, HubFactory hubFactory, IClock clock)
    {
        this.currentUserName = currentUserName;
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public async Task<string> Execute(StoreObjectRequest storeRequest, CancellationToken stoppingToken)
    {
        var storageName = await new SystemStorageName(currentUserName, storeRequest.StorageName).Value();
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
