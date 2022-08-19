// Generated Code
namespace XTI_HubAppClient;
public sealed partial class SystemGroup : AppClientGroup
{
    public SystemGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "System")
    {
        Actions = new SystemGroupActions(GetAppContext: CreatePostAction<GetAppContextRequest, AppContextModel>("GetAppContext"), GetUserContext: CreatePostAction<GetUserContextRequest, UserContextModel>("GetUserContext"), AddOrUpdateModifierByTargetKey: CreatePostAction<AddOrUpdateModifierByTargetKeyRequest, ModifierModel>("AddOrUpdateModifierByTargetKey"), StoreObject: CreatePostAction<StoreObjectRequest, string>("StoreObject"), GetStoredObject: CreatePostAction<GetStoredObjectRequest, string>("GetStoredObject"));
    }

    public SystemGroupActions Actions { get; }

    public Task<AppContextModel> GetAppContext(GetAppContextRequest model, CancellationToken ct = default) => Actions.GetAppContext.Post("", model, ct);
    public Task<UserContextModel> GetUserContext(GetUserContextRequest model, CancellationToken ct = default) => Actions.GetUserContext.Post("", model, ct);
    public Task<ModifierModel> AddOrUpdateModifierByTargetKey(AddOrUpdateModifierByTargetKeyRequest model, CancellationToken ct = default) => Actions.AddOrUpdateModifierByTargetKey.Post("", model, ct);
    public Task<string> StoreObject(StoreObjectRequest model, CancellationToken ct = default) => Actions.StoreObject.Post("", model, ct);
    public Task<string> GetStoredObject(GetStoredObjectRequest model, CancellationToken ct = default) => Actions.GetStoredObject.Post("", model, ct);
    public sealed record SystemGroupActions(AppClientPostAction<GetAppContextRequest, AppContextModel> GetAppContext, AppClientPostAction<GetUserContextRequest, UserContextModel> GetUserContext, AppClientPostAction<AddOrUpdateModifierByTargetKeyRequest, ModifierModel> AddOrUpdateModifierByTargetKey, AppClientPostAction<StoreObjectRequest, string> StoreObject, AppClientPostAction<GetStoredObjectRequest, string> GetStoredObject);
}