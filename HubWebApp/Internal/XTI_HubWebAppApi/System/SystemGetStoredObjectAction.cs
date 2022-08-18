using XTI_HubWebAppApi.Storage;

namespace XTI_HubWebAppApi.System;

internal sealed class SystemGetStoredObjectAction : AppAction<GetStoredObjectRequest, string>
{
    private readonly ICurrentUserName currentUserName;
    private readonly StoredObjectFactory storedObjectFactory;

    public SystemGetStoredObjectAction(ICurrentUserName currentUserName, StoredObjectFactory storedObjectFactory)
    {
        this.currentUserName = currentUserName;
        this.storedObjectFactory = storedObjectFactory; 
    }

    public async Task<string> Execute(GetStoredObjectRequest model, CancellationToken stoppingToken)
    {
        var storageName = await new SystemStorageName(currentUserName, model.StorageName).Value();
        var storedObject = storedObjectFactory.CreateStoredObject(storageName);
        var data = await storedObject.SerializedValue(model.StorageKey);
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new StoredObjectNotFoundException(model.StorageName, model.StorageKey);
        }
        return data;
    }
}
