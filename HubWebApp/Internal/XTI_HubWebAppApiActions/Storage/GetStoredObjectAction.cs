using XTI_Core;

namespace XTI_HubWebAppApiActions.Storage;

public sealed class GetStoredObjectAction : AppAction<GetStoredObjectRequest, string>
{
    private readonly EfHubDB hubFactory;
    private readonly IClock clock;
    private readonly HubWebAppOptions options;

    public GetStoredObjectAction(EfHubDB hubFactory, IClock clock, HubWebAppOptions options)
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
            options.Storage.SingleUseExpirationInSeconds,
            stoppingToken
        );
        return data;
    }
}
