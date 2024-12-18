namespace XTI_HubWebAppApi.Storage;

partial class StorageGroupBuilder
{
    partial void Configure()
    {
        StoreObject.WithAllowed(HubInfo.Roles.AddStoredObject);
        GetStoredObject.AllowAnonymousAccess();
    }
}
