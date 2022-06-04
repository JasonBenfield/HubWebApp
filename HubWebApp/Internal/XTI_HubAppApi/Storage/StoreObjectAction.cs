namespace XTI_HubAppApi.Storage;

internal sealed class StoreObjectAction : AppAction<StoreObjectRequest, string>
{
    private readonly StoredObjectFactory storedObjectFactory;

    public StoreObjectAction(StoredObjectFactory storedObjectFactory)
    {
        this.storedObjectFactory = storedObjectFactory;
    }

    public Task<string> Execute(StoreObjectRequest model, CancellationToken stoppingToken) =>
        storedObjectFactory.CreateStoredObject(new StorageName(model.StorageName)).Store
        (
            model.GeneratedStorageKeyType, 
            model.Data, 
            model.ExpireAfter
        );
}
