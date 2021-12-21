// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class AppUserMaintenanceController : Controller
{
    private readonly HubAppApi api;
    public AppUserMaintenanceController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<int>> AssignRole([FromBody] UserRoleRequest model)
    {
        return api.Group("AppUserMaintenance").Action<UserRoleRequest, int>("AssignRole").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> UnassignRole([FromBody] UserRoleRequest model)
    {
        return api.Group("AppUserMaintenance").Action<UserRoleRequest, EmptyActionResult>("UnassignRole").Execute(model);
    }
}