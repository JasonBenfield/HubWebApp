using XTI_Core;

namespace XTI_HubWebAppApiActions.Storage;

public sealed class GetStoredObjectAction : AppAction<GetStoredObjectRequest, string>
{
    private readonly HubFactory hubFactory;
    private readonly IClock clock;
    private readonly HubWebAppOptions options;

    public GetStoredObjectAction(HubFactory hubFactory, IClock clock, HubWebAppOptions options)
    {
        this.hubFactory = hubFactory;
        this.clock = clock;
        this.options = options;
    }

    public async Task<string> Execute(GetStoredObjectRequest requestData, CancellationToken stoppingToken)
    {
        var storageName = new StorageName(requestData.StorageName);
        var data = await hubFactory.StoredObjects.SerializedStoredObject
        (
            storageName, 
            requestData.StorageKey, 
            clock.Now(), 
            options.Storage.SingleUseExpirationInSeconds
        );
        return data;
    }
}
