// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class SystemController : Controller
{
    private readonly HubAppApi api;
    public SystemController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppContextModel>> GetAppContext([FromBody] GetAppContextRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<GetAppContextRequest, AppContextModel>("GetAppContext").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel>> GetModifier([FromBody] GetModifierRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<GetModifierRequest, ModifierModel>("GetModifier").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel>> AddOrUpdateModifierByTargetKey([FromBody] SystemAddOrUpdateModifierByTargetKeyRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<SystemAddOrUpdateModifierByTargetKeyRequest, ModifierModel>("AddOrUpdateModifierByTargetKey").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel>> AddOrUpdateModifierByModKey([FromBody] SystemAddOrUpdateModifierByModKeyRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<SystemAddOrUpdateModifierByModKeyRequest, ModifierModel>("AddOrUpdateModifierByModKey").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetUserOrAnon([FromBody] AppUserNameRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<AppUserNameRequest, AppUserModel>("GetUserOrAnon").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetUserRoles([FromBody] GetUserRolesRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<GetUserRolesRequest, AppRoleModel[]>("GetUserRoles").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<UserAuthenticatorModel[]>> GetUserAuthenticators([FromBody] AppUserIDRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<AppUserIDRequest, UserAuthenticatorModel[]>("GetUserAuthenticators").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel[]>> GetUsersWithAnyRole([FromBody] SystemGetUsersWithAnyRoleRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<SystemGetUsersWithAnyRoleRequest, AppUserModel[]>("GetUsersWithAnyRole").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<string>> StoreObject([FromBody] StoreObjectRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<StoreObjectRequest, string>("StoreObject").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<string>> GetStoredObject([FromBody] GetStoredObjectRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<GetStoredObjectRequest, string>("GetStoredObject").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> SetUserAccess([FromBody] SystemSetUserAccessRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<SystemSetUserAccessRequest, EmptyActionResult>("SetUserAccess").Execute(model, ct);
    }
}