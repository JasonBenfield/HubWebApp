// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class AppUserMaintenanceController : Controller
{
    private readonly HubAppApi api;
    public AppUserMaintenanceController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<int>> AssignRole([FromBody] UserRoleRequest model, CancellationToken ct)
    {
        return api.Group("AppUserMaintenance").Action<UserRoleRequest, int>("AssignRole").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> UnassignRole([FromBody] UserRoleRequest model, CancellationToken ct)
    {
        return api.Group("AppUserMaintenance").Action<UserRoleRequest, EmptyActionResult>("UnassignRole").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> DenyAccess([FromBody] UserModifierKey model, CancellationToken ct)
    {
        return api.Group("AppUserMaintenance").Action<UserModifierKey, EmptyActionResult>("DenyAccess").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> AllowAccess([FromBody] UserModifierKey model, CancellationToken ct)
    {
        return api.Group("AppUserMaintenance").Action<UserModifierKey, EmptyActionResult>("AllowAccess").Execute(model, ct);
    }
}