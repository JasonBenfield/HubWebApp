// Generated Code
namespace XTI_HubAppClient;
public sealed partial class SystemGroup : AppClientGroup
{
    public SystemGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "System")
    {
        Actions = new SystemGroupActions(AddOrUpdateModifierByModKey: CreatePostAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel>("AddOrUpdateModifierByModKey"), AddOrUpdateModifierByTargetKey: CreatePostAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel>("AddOrUpdateModifierByTargetKey"), GetAppContext: CreatePostAction<GetAppContextRequest, AppContextModel>("GetAppContext"), GetModifier: CreatePostAction<GetModifierRequest, ModifierModel>("GetModifier"), GetStoredObject: CreatePostAction<GetStoredObjectRequest, string>("GetStoredObject"), GetUserAuthenticators: CreatePostAction<AppUserIDRequest, UserAuthenticatorModel[]>("GetUserAuthenticators"), GetUserByUserName: CreatePostAction<AppUserNameRequest, AppUserModel>("GetUserByUserName"), GetUserOrAnon: CreatePostAction<AppUserNameRequest, AppUserModel>("GetUserOrAnon"), GetUserRoles: CreatePostAction<GetUserRolesRequest, AppRoleModel[]>("GetUserRoles"), GetUsersWithAnyRole: CreatePostAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]>("GetUsersWithAnyRole"), SetUserAccess: CreatePostAction<SystemSetUserAccessRequest, EmptyActionResult>("SetUserAccess"), StoreObject: CreatePostAction<StoreObjectRequest, string>("StoreObject"));
        Configure();
    }

    partial void Configure();
    public SystemGroupActions Actions { get; }

    public Task<ModifierModel> AddOrUpdateModifierByModKey(SystemAddOrUpdateModifierByModKeyRequest requestData, CancellationToken ct = default) => Actions.AddOrUpdateModifierByModKey.Post("", requestData, ct);
    public Task<ModifierModel> AddOrUpdateModifierByTargetKey(SystemAddOrUpdateModifierByTargetKeyRequest requestData, CancellationToken ct = default) => Actions.AddOrUpdateModifierByTargetKey.Post("", requestData, ct);
    public Task<AppContextModel> GetAppContext(GetAppContextRequest requestData, CancellationToken ct = default) => Actions.GetAppContext.Post("", requestData, ct);
    public Task<ModifierModel> GetModifier(GetModifierRequest requestData, CancellationToken ct = default) => Actions.GetModifier.Post("", requestData, ct);
    public Task<string> GetStoredObject(GetStoredObjectRequest requestData, CancellationToken ct = default) => Actions.GetStoredObject.Post("", requestData, ct);
    public Task<UserAuthenticatorModel[]> GetUserAuthenticators(AppUserIDRequest requestData, CancellationToken ct = default) => Actions.GetUserAuthenticators.Post("", requestData, ct);
    public Task<AppUserModel> GetUserByUserName(AppUserNameRequest requestData, CancellationToken ct = default) => Actions.GetUserByUserName.Post("", requestData, ct);
    public Task<AppUserModel> GetUserOrAnon(AppUserNameRequest requestData, CancellationToken ct = default) => Actions.GetUserOrAnon.Post("", requestData, ct);
    public Task<AppRoleModel[]> GetUserRoles(GetUserRolesRequest requestData, CancellationToken ct = default) => Actions.GetUserRoles.Post("", requestData, ct);
    public Task<AppUserModel[]> GetUsersWithAnyRole(SystemGetUsersWithAnyRoleRequest requestData, CancellationToken ct = default) => Actions.GetUsersWithAnyRole.Post("", requestData, ct);
    public Task<EmptyActionResult> SetUserAccess(SystemSetUserAccessRequest requestData, CancellationToken ct = default) => Actions.SetUserAccess.Post("", requestData, ct);
    public Task<string> StoreObject(StoreObjectRequest requestData, CancellationToken ct = default) => Actions.StoreObject.Post("", requestData, ct);
    public sealed record SystemGroupActions(AppClientPostAction<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel> AddOrUpdateModifierByModKey, AppClientPostAction<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel> AddOrUpdateModifierByTargetKey, AppClientPostAction<GetAppContextRequest, AppContextModel> GetAppContext, AppClientPostAction<GetModifierRequest, ModifierModel> GetModifier, AppClientPostAction<GetStoredObjectRequest, string> GetStoredObject, AppClientPostAction<AppUserIDRequest, UserAuthenticatorModel[]> GetUserAuthenticators, AppClientPostAction<AppUserNameRequest, AppUserModel> GetUserByUserName, AppClientPostAction<AppUserNameRequest, AppUserModel> GetUserOrAnon, AppClientPostAction<GetUserRolesRequest, AppRoleModel[]> GetUserRoles, AppClientPostAction<SystemGetUsersWithAnyRoleRequest, AppUserModel[]> GetUsersWithAnyRole, AppClientPostAction<SystemSetUserAccessRequest, EmptyActionResult> SetUserAccess, AppClientPostAction<StoreObjectRequest, string> StoreObject);
}