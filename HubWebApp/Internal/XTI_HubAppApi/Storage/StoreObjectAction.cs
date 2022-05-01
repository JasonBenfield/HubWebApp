namespace XTI_HubAppApi.Storage;

internal sealed class StoreObjectAction : AppAction<StoreObjectRequest, string>
{
    private readonly HubFactory appFactory;

    public StoreObjectAction(HubFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public Task<string> Execute(StoreObjectRequest model) =>
        new StoreObjectProcess(appFactory).Run
        (
            new StorageName(model.StorageName), 
            model.GeneratedStorageKeyType, 
            model.Data, 
            model.TimeExpires
        );
}
