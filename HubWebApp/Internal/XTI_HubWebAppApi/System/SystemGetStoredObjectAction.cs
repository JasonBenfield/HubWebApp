using XTI_Core;

namespace XTI_HubWebAppApi.System;

internal sealed class SystemGetStoredObjectAction : AppAction<GetStoredObjectRequest, string>
{
    private readonly ICurrentUserName currentUserName;
    private readonly HubFactory hubFactory;
    private readonly IClock clock;

    public SystemGetStoredObjectAction(ICurrentUserName currentUserName, HubFactory hubFactory, IClock clock)
    {
        this.currentUserName = currentUserName;
        this.hubFactory = hubFactory;
        this.clock = clock;
    }

    public async Task<string> Execute(GetStoredObjectRequest model, CancellationToken stoppingToken)
    {
        var storageName = await new SystemStorageName(currentUserName, model.StorageName).Value();
        var data = await hubFactory.StoredObjects.SerializedStoredObject(storageName, model.StorageKey, clock.Now());
        return data;
    }
}
