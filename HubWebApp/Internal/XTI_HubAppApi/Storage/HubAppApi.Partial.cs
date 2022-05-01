using XTI_HubAppApi.Storage;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private StorageGroup? _Storage;

    public StorageGroup Storage
    {
        get => _Storage ?? throw new ArgumentNullException(nameof(_Storage));
    }

    partial void createStorageGroup(IServiceProvider sp)
    {
        _Storage = new StorageGroup
        (
            source.AddGroup(nameof(Storage)),
            sp
        );
    }
}