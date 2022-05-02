namespace XTI_HubAppApi.Storage;

public sealed class StorageGroup : AppApiGroupWrapper
{
    public StorageGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new AppApiActionFactory(source);
        StoreObject = source.AddAction
        (
            actions.Action
            (
                nameof(StoreObject),
                Access.WithAllowed(HubInfo.Roles.AddStoredObject),
                () => sp.GetRequiredService<StoreObjectValidation>(),
                () => sp.GetRequiredService<StoreObjectAction>()
            )
        );
        GetStoredObject = source.AddAction
        (
            actions.Action
            (
                nameof(GetStoredObject),
                ResourceAccess.AllowAnonymous(),
                () => sp.GetRequiredService<GetStoredObjectValidation>(),
                () => sp.GetRequiredService<GetStoredObjectAction>()
            )
        );
    }

    public AppApiAction<StoreObjectRequest, string> StoreObject { get; }
    public AppApiAction<GetStoredObjectRequest, string> GetStoredObject { get; }
}