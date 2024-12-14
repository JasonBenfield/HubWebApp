// Generated Code
namespace XTI_HubAppClient;
public sealed partial class SystemGroup : AppClientGroup
{
    public SystemGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "System")
    {
        Actions = new SystemGroupActions(AddOrUpdateModifierByModKey: CreatePostAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel>("AddOrUpdateModifierByModKey"), AddOrUpdateModifierByTargetKey: CreatePostAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel>("AddOrUpdateModifierByTargetKey"), GetAppContext: CreatePostAction<GetAppContextRequest, AppContextModel>("GetAppContext"), GetModifier: CreatePostAction<GetModifierRequest, ModifierModel>("GetModifier"), GetUserAuthenticators: CreatePostAction<AppUserIDRequest, UserAuthenticatorModel[]>("GetUserAuthenticators"), GetUserByUserName: CreatePostAction<AppUserNameRequest, AppUserModel>("GetUserByUserName"), GetUserOrAnon: CreatePostAction<AppUserNameRequest, AppUserModel>("GetUserOrAnon"), GetUserRoles: CreatePostAction<GetUserRolesRequest, AppRoleModel[]>("GetUserRoles"), GetUsersWithAnyRole: CreatePostAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]>("GetUsersWithAnyRole"), SystemGetStoredObject: CreatePostAction<GetStoredObjectRequest, string>("SystemGetStoredObject"), SystemSetUserAccess: CreatePostAction<SystemSetUserAccessRequest, EmptyActionResult>("SystemSetUserAccess"), SystemStoreObject: CreatePostAction<StoreObjectRequest, string>("SystemStoreObject"));
    }

    public SystemGroupActions Actions { get; }

    public Task<ModifierModel> AddOrUpdateModifierByModKey(SystemAddOrUpdateModifierByModKeyRequest model, CancellationToken ct = default) => Actions.AddOrUpdateModifierByModKey.Post("", model, ct);
    public Task<ModifierModel> AddOrUpdateModifierByTargetKey(SystemAddOrUpdateModifierByTargetKeyRequest model, CancellationToken ct = default) => Actions.AddOrUpdateModifierByTargetKey.Post("", model, ct);
    public Task<AppContextModel> GetAppContext(GetAppContextRequest model, CancellationToken ct = default) => Actions.GetAppContext.Post("", model, ct);
    public Task<ModifierModel> GetModifier(GetModifierRequest model, CancellationToken ct = default) => Actions.GetModifier.Post("", model, ct);
    public Task<UserAuthenticatorModel[]> GetUserAuthenticators(AppUserIDRequest model, CancellationToken ct = default) => Actions.GetUserAuthenticators.Post("", model, ct);
    public Task<AppUserModel> GetUserByUserName(AppUserNameRequest model, CancellationToken ct = default) => Actions.GetUserByUserName.Post("", model, ct);
    public Task<AppUserModel> GetUserOrAnon(AppUserNameRequest model, CancellationToken ct = default) => Actions.GetUserOrAnon.Post("", model, ct);
    public Task<AppRoleModel[]> GetUserRoles(GetUserRolesRequest model, CancellationToken ct = default) => Actions.GetUserRoles.Post("", model, ct);
    public Task<AppUserModel[]> GetUsersWithAnyRole(SystemGetUsersWithAnyRoleRequest model, CancellationToken ct = default) => Actions.GetUsersWithAnyRole.Post("", model, ct);
    public Task<string> SystemGetStoredObject(GetStoredObjectRequest model, CancellationToken ct = default) => Actions.SystemGetStoredObject.Post("", model, ct);
    public Task<EmptyActionResult> SystemSetUserAccess(SystemSetUserAccessRequest model, CancellationToken ct = default) => Actions.SystemSetUserAccess.Post("", model, ct);
    public Task<string> SystemStoreObject(StoreObjectRequest model, CancellationToken ct = default) => Actions.SystemStoreObject.Post("", model, ct);
    public sealed record SystemGroupActions(AppClientPostAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel> AddOrUpdateModifierByModKey, AppClientPostAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel> AddOrUpdateModifierByTargetKey, AppClientPostAction<GetAppContextRequest, AppContextModel> GetAppContext, AppClientPostAction<GetModifierRequest, ModifierModel> GetModifier, AppClientPostAction<AppUserIDRequest, UserAuthenticatorModel[]> GetUserAuthenticators, AppClientPostAction<AppUserNameRequest, AppUserModel> GetUserByUserName, AppClientPostAction<AppUserNameRequest, AppUserModel> GetUserOrAnon, AppClientPostAction<GetUserRolesRequest, AppRoleModel[]> GetUserRoles, AppClientPostAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]> GetUsersWithAnyRole, AppClientPostAction<GetStoredObjectRequest, string> SystemGetStoredObject, AppClientPostAction<SystemSetUserAccessRequest, EmptyActionResult> SystemSetUserAccess, AppClientPostAction<StoreObjectRequest, string> SystemStoreObject);
}