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
    public Task<ResultContainer<EmptyActionResult>> AllowAccess([FromBody] UserModifierKey requestData, CancellationToken ct)
    {
        return api.AppUserMaintenance.AllowAccess.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<int>> AssignRole([FromBody] UserRoleRequest requestData, CancellationToken ct)
    {
        return api.AppUserMaintenance.AssignRole.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> DenyAccess([FromBody] UserModifierKey requestData, CancellationToken ct)
    {
        return api.AppUserMaintenance.DenyAccess.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> UnassignRole([FromBody] UserRoleRequest requestData, CancellationToken ct)
    {
        return api.AppUserMaintenance.UnassignRole.Execute(requestData, ct);
    }
}