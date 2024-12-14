// Generated Code
namespace XTI_HubAppClient;
public sealed partial class StorageGroup : AppClientGroup
{
    public StorageGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Storage")
    {
        Actions = new StorageGroupActions(GetStoredObject: CreatePostAction<GetStoredObjectRequest, string>("GetStoredObject"), StoreObject: CreatePostAction<StoreObjectRequest, string>("StoreObject"));
    }

    public StorageGroupActions Actions { get; }

    public Task<string> GetStoredObject(GetStoredObjectRequest model, CancellationToken ct = default) => Actions.GetStoredObject.Post("", model, ct);
    public Task<string> StoreObject(StoreObjectRequest model, CancellationToken ct = default) => Actions.StoreObject.Post("", model, ct);
    public sealed record StorageGroupActions(AppClientPostAction<GetStoredObjectRequest, string> GetStoredObject, AppClientPostAction<StoreObjectRequest, string> StoreObject);
}