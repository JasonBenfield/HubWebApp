// Generated Code
namespace XTI_HubAppClient;
public sealed partial class StorageGroup : AppClientGroup
{
    public StorageGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Storage")
    {
        Actions = new StorageGroupActions(StoreObject: CreatePostAction<StoreObjectRequest, string>("StoreObject"), GetStoredObject: CreatePostAction<GetStoredObjectRequest, string>("GetStoredObject"));
    }

    public StorageGroupActions Actions { get; }

    public Task<string> StoreObject(StoreObjectRequest model) => Actions.StoreObject.Post("", model);
    public Task<string> GetStoredObject(GetStoredObjectRequest model) => Actions.GetStoredObject.Post("", model);
    public sealed record StorageGroupActions(AppClientPostAction<StoreObjectRequest, string> StoreObject, AppClientPostAction<GetStoredObjectRequest, string> GetStoredObject);
}