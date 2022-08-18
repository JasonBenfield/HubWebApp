// Generated Code
namespace XTI_HubAppClient;
public sealed partial class SystemGroup : AppClientGroup
{
    public SystemGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "System")
    {
        Actions = new SystemGroupActions(GetAppContext: CreatePostAction<GetAppContextRequest, AppContextModel>("GetAppContext"), GetUserContext: CreatePostAction<GetUserContextRequest, UserContextModel>("GetUserContext"), AddOrUpdateModifierByTargetKey: CreatePostAction<AddOrUpdateModifierByTargetKeyRequest, ModifierModel>("AddOrUpdateModifierByTargetKey"), StoreObject: CreatePostAction<StoreObjectRequest, string>("StoreObject"), GetStoredObject: CreatePostAction<GetStoredObjectRequest, string>("GetStoredObject"));
    }

    public SystemGroupActions Actions { get; }

    public Task<AppContextModel> GetAppContext(GetAppContextRequest model) => Actions.GetAppContext.Post("", model);
    public Task<UserContextModel> GetUserContext(GetUserContextRequest model) => Actions.GetUserContext.Post("", model);
    public Task<ModifierModel> AddOrUpdateModifierByTargetKey(AddOrUpdateModifierByTargetKeyRequest model) => Actions.AddOrUpdateModifierByTargetKey.Post("", model);
    public Task<string> StoreObject(StoreObjectRequest model) => Actions.StoreObject.Post("", model);
    public Task<string> GetStoredObject(GetStoredObjectRequest model) => Actions.GetStoredObject.Post("", model);
    public sealed record SystemGroupActions(AppClientPostAction<GetAppContextRequest, AppContextModel> GetAppContext, AppClientPostAction<GetUserContextRequest, UserContextModel> GetUserContext, AppClientPostAction<AddOrUpdateModifierByTargetKeyRequest, ModifierModel> AddOrUpdateModifierByTargetKey, AppClientPostAction<StoreObjectRequest, string> StoreObject, AppClientPostAction<GetStoredObjectRequest, string> GetStoredObject);
}