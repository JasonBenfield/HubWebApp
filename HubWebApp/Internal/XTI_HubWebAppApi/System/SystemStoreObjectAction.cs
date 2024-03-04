namespace XTI_HubWebAppApi.System;

internal sealed class SystemStoreObjectAction : AppAction<StoreObjectRequest, string>
{
    private readonly ICurrentUserName currentUserName;
    private readonly StoredObjectFactory storedObjectFactory;

    public SystemStoreObjectAction(ICurrentUserName currentUserName, StoredObjectFactory storedObjectFactory)
    {
        this.currentUserName = currentUserName;
        this.storedObjectFactory = storedObjectFactory;
    }

    public async Task<string> Execute(StoreObjectRequest storeRequest, CancellationToken stoppingToken)
    {
        var storageName = await new SystemStorageName(currentUserName, storeRequest.StorageName).Value();
        var storedObject = storedObjectFactory.CreateStoredObject(storageName);
        string storageKey;
        if (storeRequest.IsSingleUse)
        {
            storageKey = await storedObject.StoreSingleUse
            (
                storeRequest.GenerateKey,
                storeRequest.Data,
                storeRequest.ExpireAfter
            );
        }
        else
        {
            storageKey = await storedObject.Store
            (
                storeRequest.GenerateKey,
                storeRequest.Data,
                storeRequest.ExpireAfter,
                storeRequest.IsSlidingExpiration
            );
        }
        return storageKey;
    }
}
