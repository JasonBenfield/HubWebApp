// Generated Code
namespace XTI_HubAppClient;
public sealed partial class SystemGroup : AppClientGroup
{
    public SystemGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "System")
    {
        Actions = new SystemGroupActions(GetAppContext: CreatePostAction<GetAppContextRequest, AppContextModel>("GetAppContext"), GetModifier: CreatePostAction<GetModifierRequest, ModifierModel>("GetModifier"), AddOrUpdateModifierByTargetKey: CreatePostAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel>("AddOrUpdateModifierByTargetKey"), AddOrUpdateModifierByModKey: CreatePostAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel>("AddOrUpdateModifierByModKey"), GetUserOrAnon: CreatePostAction<AppUserNameRequest, AppUserModel>("GetUserOrAnon"), GetUserRoles: CreatePostAction<GetUserRolesRequest, AppRoleModel[]>("GetUserRoles"), GetUserAuthenticators: CreatePostAction<AppUserIDRequest, UserAuthenticatorModel[]>("GetUserAuthenticators"), GetUsersWithAnyRole: CreatePostAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]>("GetUsersWithAnyRole"), StoreObject: CreatePostAction<StoreObjectRequest, string>("StoreObject"), GetStoredObject: CreatePostAction<GetStoredObjectRequest, string>("GetStoredObject"), SetUserAccess: CreatePostAction<SystemSetUserAccessRequest, EmptyActionResult>("SetUserAccess"));
    }

    public SystemGroupActions Actions { get; }

    public Task<AppContextModel> GetAppContext(GetAppContextRequest model, CancellationToken ct = default) => Actions.GetAppContext.Post("", model, ct);
    public Task<ModifierModel> GetModifier(GetModifierRequest model, CancellationToken ct = default) => Actions.GetModifier.Post("", model, ct);
    public Task<ModifierModel> AddOrUpdateModifierByTargetKey(SystemAddOrUpdateModifierByTargetKeyRequest model, CancellationToken ct = default) => Actions.AddOrUpdateModifierByTargetKey.Post("", model, ct);
    public Task<ModifierModel> AddOrUpdateModifierByModKey(SystemAddOrUpdateModifierByModKeyRequest model, CancellationToken ct = default) => Actions.AddOrUpdateModifierByModKey.Post("", model, ct);
    public Task<AppUserModel> GetUserOrAnon(AppUserNameRequest model, CancellationToken ct = default) => Actions.GetUserOrAnon.Post("", model, ct);
    public Task<AppRoleModel[]> GetUserRoles(GetUserRolesRequest model, CancellationToken ct = default) => Actions.GetUserRoles.Post("", model, ct);
    public Task<UserAuthenticatorModel[]> GetUserAuthenticators(AppUserIDRequest model, CancellationToken ct = default) => Actions.GetUserAuthenticators.Post("", model, ct);
    public Task<AppUserModel[]> GetUsersWithAnyRole(SystemGetUsersWithAnyRoleRequest model, CancellationToken ct = default) => Actions.GetUsersWithAnyRole.Post("", model, ct);
    public Task<string> StoreObject(StoreObjectRequest model, CancellationToken ct = default) => Actions.StoreObject.Post("", model, ct);
    public Task<string> GetStoredObject(GetStoredObjectRequest model, CancellationToken ct = default) => Actions.GetStoredObject.Post("", model, ct);
    public Task<EmptyActionResult> SetUserAccess(SystemSetUserAccessRequest model, CancellationToken ct = default) => Actions.SetUserAccess.Post("", model, ct);
    public sealed record SystemGroupActions(AppClientPostAction<GetAppContextRequest, AppContextModel> GetAppContext, AppClientPostAction<GetModifierRequest, ModifierModel> GetModifier, AppClientPostAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel> AddOrUpdateModifierByTargetKey, AppClientPostAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel> AddOrUpdateModifierByModKey, AppClientPostAction<AppUserNameRequest, AppUserModel> GetUserOrAnon, AppClientPostAction<GetUserRolesRequest, AppRoleModel[]> GetUserRoles, AppClientPostAction<AppUserIDRequest, UserAuthenticatorModel[]> GetUserAuthenticators, AppClientPostAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]> GetUsersWithAnyRole, AppClientPostAction<StoreObjectRequest, string> StoreObject, AppClientPostAction<GetStoredObjectRequest, string> GetStoredObject, AppClientPostAction<SystemSetUserAccessRequest, EmptyActionResult> SetUserAccess);
}