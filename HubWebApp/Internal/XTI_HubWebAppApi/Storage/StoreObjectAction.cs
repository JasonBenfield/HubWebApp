namespace XTI_HubWebAppApi.Storage;

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
            model.GenerateKey, 
            model.Data, 
            model.ExpireAfter
        );
}
