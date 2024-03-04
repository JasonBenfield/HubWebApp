namespace XTI_HubWebAppApi.Storage;

internal sealed class StoreObjectAction : AppAction<StoreObjectRequest, string>
{
    private readonly StoredObjectFactory storedObjectFactory;

    public StoreObjectAction(StoredObjectFactory storedObjectFactory)
    {
        this.storedObjectFactory = storedObjectFactory;
    }

    public async Task<string> Execute(StoreObjectRequest storeRequest, CancellationToken stoppingToken)
    {
        var storedObject = storedObjectFactory.CreateStoredObject(new StorageName(storeRequest.StorageName));
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
