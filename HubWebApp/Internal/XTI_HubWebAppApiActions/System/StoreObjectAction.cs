using XTI_Core;

namespace XTI_HubWebAppApiActions.System;

public sealed class StoreObjectAction : AppAction<StoreObjectRequest, string>
{
    private readonly ICurrentUserName currentUserName;
    private readonly EfHubDB db;
    private readonly IClock clock;

    public StoreObjectAction(ICurrentUserName currentUserName, EfHubDB db, IClock clock)
    {
        this.currentUserName = currentUserName;
        this.db = db;
        this.clock = clock;
    }

    public async Task<string> Execute(StoreObjectRequest storeRequest, CancellationToken stoppingToken)
    {
        var storageName = await new SystemStorageName(currentUserName, storeRequest.StorageName).Value();
        string storageKey;
        if (storeRequest.IsSingleUse)
        {
            storageKey = await db.StoredObjects.StoreSingleUse
            (
                storageName,
                storeRequest.GenerateKey,
                storeRequest.Data,
                clock,
                storeRequest.ExpireAfter,
                stoppingToken
            );
        }
        else
        {
            storageKey = await db.StoredObjects.Store
            (
                storageName,
                storeRequest.GenerateKey,
                storeRequest.Data,
                clock,
                storeRequest.ExpireAfter,
                storeRequest.IsSlidingExpiration,
                stoppingToken
            );
        }
        return storageKey;
    }
}
