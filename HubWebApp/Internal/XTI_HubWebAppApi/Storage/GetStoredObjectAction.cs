using XTI_Core;

namespace XTI_HubWebAppApi.Storage;

internal sealed class GetStoredObjectAction : AppAction<GetStoredObjectRequest, string>
{
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public GetStoredObjectAction(HubFactory hubFactory, IClock clock)
    {
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public async Task<string> Execute(GetStoredObjectRequest model, CancellationToken stoppingToken)
    {
        var storageName = new StorageName(model.StorageName);
        var data = await hubFactory.StoredObjects.SerializedStoredObject(storageName, model.StorageKey, clock.Now());
        return data;
    }
}
