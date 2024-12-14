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
    public Task<ResultContainer<ModifierModel>> AddOrUpdateModifierByModKey([FromBody] SystemAddOrUpdateModifierByModKeyRequest requestData, CancellationToken ct)
    {
        return api.System.AddOrUpdateModifierByModKey.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel>> AddOrUpdateModifierByTargetKey([FromBody] SystemAddOrUpdateModifierByTargetKeyRequest requestData, CancellationToken ct)
    {
        return api.System.AddOrUpdateModifierByTargetKey.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppContextModel>> GetAppContext([FromBody] GetAppContextRequest requestData, CancellationToken ct)
    {
        return api.System.GetAppContext.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel>> GetModifier([FromBody] GetModifierRequest requestData, CancellationToken ct)
    {
        return api.System.GetModifier.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<UserAuthenticatorModel[]>> GetUserAuthenticators([FromBody] AppUserIDRequest requestData, CancellationToken ct)
    {
        return api.System.GetUserAuthenticators.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetUserByUserName([FromBody] AppUserNameRequest requestData, CancellationToken ct)
    {
        return api.System.GetUserByUserName.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetUserOrAnon([FromBody] AppUserNameRequest requestData, CancellationToken ct)
    {
        return api.System.GetUserOrAnon.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetUserRoles([FromBody] GetUserRolesRequest requestData, CancellationToken ct)
    {
        return api.System.GetUserRoles.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel[]>> GetUsersWithAnyRole([FromBody] SystemGetUsersWithAnyRoleRequest requestData, CancellationToken ct)
    {
        return api.System.GetUsersWithAnyRole.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<string>> SystemGetStoredObject([FromBody] GetStoredObjectRequest requestData, CancellationToken ct)
    {
        return api.System.SystemGetStoredObject.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> SystemSetUserAccess([FromBody] SystemSetUserAccessRequest requestData, CancellationToken ct)
    {
        return api.System.SystemSetUserAccess.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<string>> SystemStoreObject([FromBody] StoreObjectRequest requestData, CancellationToken ct)
    {
        return api.System.SystemStoreObject.Execute(requestData, ct);
    }
}