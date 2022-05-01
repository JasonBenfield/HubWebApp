using XTI_Core;

namespace XTI_HubAppApi.Storage;

internal sealed class GetStoredObjectAction : AppAction<GetStoredObjectRequest, string>
{
    private readonly HubFactory appFactory;
    private readonly IClock clock;

    public GetStoredObjectAction(HubFactory appFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.clock = clock;
    }

    public async Task<string> Execute(GetStoredObjectRequest model)
    {
        var storageName = new StorageName(model.StorageName);
        var data = await appFactory.StoredObjects.StoredObject(storageName, model.StorageKey, clock.Now());
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new StoredObjectNotFoundException(model.StorageName, model.StorageKey);
        }
        return data;
    }
}
