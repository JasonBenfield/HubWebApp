using XTI_Core;

namespace XTI_HubWebAppApiActions.System;

public sealed class GetStoredObjectAction : AppAction<GetStoredObjectRequest, string>
{
    private readonly ICurrentUserName currentUserName;
    private readonly HubFactory hubFactory;
    private readonly IClock clock;
    private readonly HubWebAppOptions options;

    public GetStoredObjectAction(ICurrentUserName currentUserName, HubFactory hubFactory, IClock clock, HubWebAppOptions options)
    {
        this.currentUserName = currentUserName;
        this.hubFactory = hubFactory;
        this.clock = clock;
        this.options = options;
    }

    public async Task<string> Execute(GetStoredObjectRequest requestData, CancellationToken stoppingToken)
    {
        var storageName = await new SystemStorageName(currentUserName, requestData.StorageName).Value();
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
