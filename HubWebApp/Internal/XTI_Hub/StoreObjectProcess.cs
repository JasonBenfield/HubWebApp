namespace XTI_Hub;

public sealed class StoreObjectProcess
{
    private readonly HubFactory appFactory;

    public StoreObjectProcess(HubFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public Task<string> Run(StorageName storageName, GeneratedStorageKeyType generatedStorageKeyType, string data, DateTimeOffset timeExpires)
    {
        IGeneratedStorageKey generatedStorageKey;
        if (generatedStorageKeyType.Equals(GeneratedStorageKeyType.Values.Guid))
        {
            generatedStorageKey = new GuidGeneratedStorageKey();
        }
        else
        {
            var numberOfDigits = generatedStorageKeyType.Value;
            generatedStorageKey = new RandomGeneratedStorageKey(numberOfDigits);
        }
        return appFactory.StoredObjects.AddOrUpdate
        (
            storageName,
            generatedStorageKey,
            data,
            timeExpires
        );
    }
}
