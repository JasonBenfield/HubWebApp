namespace XTI_HubWebAppApi.Storage;

internal sealed class StoreObjectAction : AppAction<StoreObjectRequest, string>
{
    private readonly StoredObjectFactory storedObjectFactory;

    public StoreObjectAction(StoredObjectFactory storedObjectFactory)
    {
        this.storedObjectFactory = storedObjectFactory;
    }

    public async Task<string> Execute(StoreObjectRequest model, CancellationToken stoppingToken)
    {
        var storedObject = storedObjectFactory.CreateStoredObject(new StorageName(model.StorageName));
        string storageKey;
        if (model.IsSingleUse)
        {
            storageKey = await storedObject.StoreSingleUse
            (
                model.GenerateKey,
                model.Data,
                model.ExpireAfter
            );
        }
        else
        {
            storageKey = await storedObject.Store
            (
                model.GenerateKey,
                model.Data,
                model.ExpireAfter
            );
        }
        return storageKey;
    }
}
