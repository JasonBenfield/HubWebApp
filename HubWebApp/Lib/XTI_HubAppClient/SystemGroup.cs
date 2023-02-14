// Generated Code
namespace XTI_HubAppClient;
public sealed partial class SystemGroup : AppClientGroup
{
    public SystemGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "System")
    {
        Actions = new SystemGroupActions(GetAppContext: CreatePostAction<GetAppContextRequest, AppContextModel>("GetAppContext"), GetUserContext: CreatePostAction<GetUserContextRequest, UserContextModel>("GetUserContext"), AddOrUpdateModifierByTargetKey: CreatePostAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel>("AddOrUpdateModifierByTargetKey"), AddOrUpdateModifierByModKey: CreatePostAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel>("AddOrUpdateModifierByModKey"), GetUserOrAnon: CreatePostAction<string, AppUserModel>("GetUserOrAnon"), GetUserAuthenticators: CreatePostAction<int, UserAuthenticatorModel[]>("GetUserAuthenticators"), GetUsersWithAnyRole: CreatePostAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]>("GetUsersWithAnyRole"), StoreObject: CreatePostAction<StoreObjectRequest, string>("StoreObject"), GetStoredObject: CreatePostAction<GetStoredObjectRequest, string>("GetStoredObject"), SetUserAccess: CreatePostAction<SystemSetUserAccessRequest, EmptyActionResult>("SetUserAccess"));
    }

    public SystemGroupActions Actions { get; }

    public Task<AppContextModel> GetAppContext(GetAppContextRequest model, CancellationToken ct = default) => Actions.GetAppContext.Post("", model, ct);
    public Task<UserContextModel> GetUserContext(GetUserContextRequest model, CancellationToken ct = default) => Actions.GetUserContext.Post("", model, ct);
    public Task<ModifierModel> AddOrUpdateModifierByTargetKey(SystemAddOrUpdateModifierByTargetKeyRequest model, CancellationToken ct = default) => Actions.AddOrUpdateModifierByTargetKey.Post("", model, ct);
    public Task<ModifierModel> AddOrUpdateModifierByModKey(SystemAddOrUpdateModifierByModKeyRequest model, CancellationToken ct = default) => Actions.AddOrUpdateModifierByModKey.Post("", model, ct);
    public Task<AppUserModel> GetUserOrAnon(string model, CancellationToken ct = default) => Actions.GetUserOrAnon.Post("", model, ct);
    public Task<UserAuthenticatorModel[]> GetUserAuthenticators(int model, CancellationToken ct = default) => Actions.GetUserAuthenticators.Post("", model, ct);
    public Task<AppUserModel[]> GetUsersWithAnyRole(SystemGetUsersWithAnyRoleRequest model, CancellationToken ct = default) => Actions.GetUsersWithAnyRole.Post("", model, ct);
    public Task<string> StoreObject(StoreObjectRequest model, CancellationToken ct = default) => Actions.StoreObject.Post("", model, ct);
    public Task<string> GetStoredObject(GetStoredObjectRequest model, CancellationToken ct = default) => Actions.GetStoredObject.Post("", model, ct);
    public Task<EmptyActionResult> SetUserAccess(SystemSetUserAccessRequest model, CancellationToken ct = default) => Actions.SetUserAccess.Post("", model, ct);
    public sealed record SystemGroupActions(AppClientPostAction<GetAppContextRequest, AppContextModel> GetAppContext, AppClientPostAction<GetUserContextRequest, UserContextModel> GetUserContext, AppClientPostAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel> AddOrUpdateModifierByTargetKey, AppClientPostAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel> AddOrUpdateModifierByModKey, AppClientPostAction<string, AppUserModel> GetUserOrAnon, AppClientPostAction<int, UserAuthenticatorModel[]> GetUserAuthenticators, AppClientPostAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]> GetUsersWithAnyRole, AppClientPostAction<StoreObjectRequest, string> StoreObject, AppClientPostAction<GetStoredObjectRequest, string> GetStoredObject, AppClientPostAction<SystemSetUserAccessRequest, EmptyActionResult> SetUserAccess);
}