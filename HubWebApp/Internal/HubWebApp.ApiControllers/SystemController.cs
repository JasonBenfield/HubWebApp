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
    public Task<ResultContainer<UserContextModel>> GetUserContext([FromBody] GetUserContextRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<GetUserContextRequest, UserContextModel>("GetUserContext").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel>> AddOrUpdateModifierByTargetKey([FromBody] AddOrUpdateModifierByTargetKeyRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<AddOrUpdateModifierByTargetKeyRequest, ModifierModel>("AddOrUpdateModifierByTargetKey").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel>> AddOrUpdateModifierByModKey([FromBody] AddOrUpdateModifierByModKeyRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<AddOrUpdateModifierByModKeyRequest, ModifierModel>("AddOrUpdateModifierByModKey").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetUserOrAnon([FromBody] string model, CancellationToken ct)
    {
        return api.Group("System").Action<string, AppUserModel>("GetUserOrAnon").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<UserAuthenticatorModel[]>> GetUserAuthenticators([FromBody] int model, CancellationToken ct)
    {
        return api.Group("System").Action<int, UserAuthenticatorModel[]>("GetUserAuthenticators").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel[]>> GetUsersWithAnyRole([FromBody] GetUsersWithAnyRoleRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<GetUsersWithAnyRoleRequest, AppUserModel[]>("GetUsersWithAnyRole").Execute(model, ct);
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
}