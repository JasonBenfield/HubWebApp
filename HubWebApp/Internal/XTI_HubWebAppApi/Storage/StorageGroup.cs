namespace XTI_HubWebAppApi.Storage;

public sealed class StorageGroup : AppApiGroupWrapper
{
    public StorageGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        StoreObject = source.AddAction
        (
            nameof(StoreObject),
            () => sp.GetRequiredService<StoreObjectAction>(),
            () => sp.GetRequiredService<StoreObjectValidation>(),
            Access.WithAllowed(HubInfo.Roles.AddStoredObject)
        );
        GetStoredObject = source.AddAction
        (
            nameof(GetStoredObject),
            () => sp.GetRequiredService<GetStoredObjectAction>(),
            () => sp.GetRequiredService<GetStoredObjectValidation>(),
            ResourceAccess.AllowAnonymous()
        );
    }

    public AppApiAction<StoreObjectRequest, string> StoreObject { get; }
    public AppApiAction<GetStoredObjectRequest, string> GetStoredObject { get; }
}