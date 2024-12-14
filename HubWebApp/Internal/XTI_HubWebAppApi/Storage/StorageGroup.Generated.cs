using XTI_HubWebAppApiActions.Storage;

// Generated Code
namespace XTI_HubWebAppApi.Storage;
public sealed partial class StorageGroup : AppApiGroupWrapper
{
    internal StorageGroup(AppApiGroup source, StorageGroupBuilder builder) : base(source)
    {
        GetStoredObject = builder.GetStoredObject.Build();
        StoreObject = builder.StoreObject.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<GetStoredObjectRequest, string> GetStoredObject { get; }
    public AppApiAction<StoreObjectRequest, string> StoreObject { get; }
}