// Generated Code
namespace XTI_HubAppClient;
public sealed partial class StorageGroup : AppClientGroup
{
    public StorageGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Storage")
    {
    }

    public Task<string> StoreObject(StoreObjectRequest model) => Post<string, StoreObjectRequest>("StoreObject", "", model);
    public Task<string> GetStoredObject(GetStoredObjectRequest model) => Post<string, GetStoredObjectRequest>("GetStoredObject", "", model);
}