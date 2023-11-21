using XTI_HubWebAppApi.Storage;

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

    public async Task<string> Execute(StoreObjectRequest model, CancellationToken stoppingToken)
    {
        var storageName = await new SystemStorageName(currentUserName, model.StorageName).Value();
        var storedObject = storedObjectFactory.CreateStoredObject(storageName);
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
