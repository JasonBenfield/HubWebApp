using XTI_HubWebAppApiActions.Storage;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Storage;
public sealed partial class StorageGroupBuilder
{
    private readonly AppApiGroup source;
    internal StorageGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        GetStoredObject = source.AddAction<GetStoredObjectRequest, string>("GetStoredObject").WithExecution<GetStoredObjectAction>().WithValidation<GetStoredObjectValidation>();
        StoreObject = source.AddAction<StoreObjectRequest, string>("StoreObject").WithExecution<StoreObjectAction>().WithValidation<StoreObjectValidation>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<GetStoredObjectRequest, string> GetStoredObject { get; }
    public AppApiActionBuilder<StoreObjectRequest, string> StoreObject { get; }

    public StorageGroup Build() => new StorageGroup(source, this);
}