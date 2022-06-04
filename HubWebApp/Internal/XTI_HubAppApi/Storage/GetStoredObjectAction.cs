namespace XTI_HubAppApi.Storage;

internal sealed class GetStoredObjectAction : AppAction<GetStoredObjectRequest, string>
{
    private readonly StoredObjectFactory storedObjectFactory;

    public GetStoredObjectAction(StoredObjectFactory storedObjectFactory)
    {
        this.storedObjectFactory = storedObjectFactory; 
    }

    public async Task<string> Execute(GetStoredObjectRequest model, CancellationToken stoppingToken)
    {
        var storageName = new StorageName(model.StorageName);
        var storedObject = storedObjectFactory.CreateStoredObject(storageName);
        var data = await storedObject.SerializedValue(model.StorageKey);
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new StoredObjectNotFoundException(model.StorageName, model.StorageKey);
        }
        return data;
    }
}
